using System.Collections.Generic;
using Core.CollisionBehaviours.Observer;
using Core.Services.Assets;
using Core.Services.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Animals
{
	public class AnimalsFactory : IAnimalsFactory
	{
		private readonly IResourcesProvider _resourceProvider;
		private readonly AnimalsConfigProvider _configsProvider;
		private readonly IObjectResolver _resolver;
		private readonly ICollisionsObserver _collisionsObserver;

		private readonly Dictionary<AnimalType, Animal> _animals = new();
		private readonly Dictionary<AnimalType, Stack<Animal>> _pools = new();

		private Transform _poolsRoot;

		public AnimalsFactory(IResourcesProvider resourceProvider,
			AnimalsConfigProvider configsProvider,
			IObjectResolver resolver,
			ICollisionsObserver collisionsObserver)
		{
			_resourceProvider = resourceProvider;
			_configsProvider = configsProvider;
			_resolver = resolver;
			_collisionsObserver = collisionsObserver;
		}

		public async UniTask Initialize()
		{
			_poolsRoot = new GameObject("AnimalPools").transform;

			var animals = await _resourceProvider.Load<Animal>(_configsProvider.GetConfig().GetAnimalIds());
			foreach (var animal in animals) _animals.Add(animal.Type, animal);

			PrewarmPools();
		}

		private void PrewarmPools()
		{
			var config = _configsProvider.GetConfig();
			var n = config.PoolPrewarmPerType;
			if (n <= 0) return;

			foreach (var pair in _animals)
			{
				var type = pair.Key;
				var template = pair.Value;
				var pool = GetPool(type);

				for (var i = 0; i < n; i++)
				{
					var instance = _resolver.Instantiate(template, Vector3.zero, Quaternion.identity);
					instance.gameObject.SetActive(false);
					instance.transform.SetParent(_poolsRoot);
					pool.Push(instance);
				}
			}
		}

		public Animal Create(AnimalType type, Vector3 position, Quaternion rotation)
		{
			var pool = GetPool(type);
			Animal animal;

			if (pool.Count > 0)
			{
				animal = pool.Pop();
				animal.transform.SetParent(null);
				animal.transform.SetPositionAndRotation(position, rotation);
				animal.gameObject.SetActive(true);
			}
			else
			{
				var template = _animals[type];
				animal = _resolver.Instantiate(template, position, rotation);
			}

			animal.Initialize(_configsProvider.GetConfig().GetAnimalData(type));
			_collisionsObserver.Register(animal);

			return animal;
		}

		public void Release(Animal animal)
		{
			if (animal == null)
				return;

			animal.Despawn();
			animal.gameObject.SetActive(false);
			animal.transform.SetParent(_poolsRoot);
			GetPool(animal.Type).Push(animal);
		}

		private Stack<Animal> GetPool(AnimalType type)
		{
			if (_pools.TryGetValue(type, out var stack))
				return stack;

			stack = new Stack<Animal>();
			_pools[type] = stack;

			return stack;
		}
	}
}