using System;
using System.Collections.Generic;
using Core.CollisionBehaviours;
using Core.Configs.Animals;
using Core.Services.Cameras;
using Core.Services.Configs;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Core.Animals.Spawner
{
	public class AnimalsSpawner : IAnimalsSpawner
	{
		public event Action<CollisionBehaviourType> OnAnimalDied;

		private readonly AnimalsConfigProvider _configsProvider;
		private readonly IAnimalsFactory _factory;
		private readonly ICameraHandler _cameraHandler;

		private float _elapsedTime;

		private readonly List<Animal> _spawnedAnimals = new();

		public AnimalsSpawner(AnimalsConfigProvider configsProvider, IAnimalsFactory factory,
			ICameraHandler cameraHandler)
		{
			_configsProvider = configsProvider;
			_factory = factory;
			_cameraHandler = cameraHandler;
		}

		private AnimalsConfig AnimalsConfig => _configsProvider.GetConfig();

		public void Tick()
		{
			_elapsedTime += Time.deltaTime;

			if (!(_elapsedTime >= AnimalsConfig.AnimalSpawnInterval)) 
				return;
			
			_elapsedTime = 0;

			if (_spawnedAnimals.Count == AnimalsConfig.MaxAnimalsOnLevel) 
				return;

			SpawnAnimal();
		}

		private void SpawnAnimal()
		{
			var randomPosition = GetRandomPoint();
			var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
			var animal = _factory.Create(GetRandomAnimalType(), randomPosition, randomRotation);
			animal.OnDeath += RemoveDiedAnimal;
			_spawnedAnimals.Add(animal);
		}

		private Vector3 GetRandomPoint()
		{
			if (_cameraHandler.Camera == null) return Vector3.zero;

			var inner = _cameraHandler.Bounds.InsetXZ(AnimalsConfig.TurnBackBoundsInset);
			if (inner.width <= 0f || inner.height <= 0f) return Vector3.zero;

			var x = Random.Range(inner.xMin, inner.xMax);
			var z = Random.Range(inner.yMin, inner.yMax);
			return new Vector3(x, AnimalsConfig.SpawnGroundHeight, z);
		}

		private void RemoveDiedAnimal(Animal animal)
		{
			animal.OnDeath -= RemoveDiedAnimal;
			_spawnedAnimals.Remove(animal);

			OnAnimalDied?.Invoke(animal.CollisionBehaviour);
		}

		private AnimalType GetRandomAnimalType()
		{
			var rng = Random.Range(0f, 1f);
			return rng >= 0.5f ? AnimalType.Frog : AnimalType.Snake;
		}
	}
}