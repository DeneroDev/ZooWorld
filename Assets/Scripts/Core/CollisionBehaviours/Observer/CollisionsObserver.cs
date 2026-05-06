using System.Collections.Generic;
using Core.Animals;
using Core.Animals.Strategy;

namespace Core.CollisionBehaviours.Observer
{
	public class CollisionsObserver : ICollisionsObserver
	{
		private readonly IStrategyResolver _strategyResolver;

		private readonly HashSet<int> _busyAnimals = new();

		public CollisionsObserver(IStrategyResolver strategyResolver)
		{
			_strategyResolver = strategyResolver;
		}
        
		public void Register(Animal animal)
		{
			animal.OnCollision += HandleCollision;
			animal.OnDeath += Unregister;
		}
        
		public void Unregister(Animal animal)
		{
			animal.OnCollision -= HandleCollision;
			animal.OnDeath -= Unregister;
		}

		private void HandleCollision(Animal animalA, Animal animalB)
		{
			if (_busyAnimals.Contains(animalA.GetHashCode()) || _busyAnimals.Contains(animalB.GetHashCode()))
			{
				return;
			}
            
			_busyAnimals.Add(animalA.GetHashCode());
			_busyAnimals.Add(animalB.GetHashCode());

			var collision = _strategyResolver.ResolveCollision(animalA.CollisionBehaviour, animalB.CollisionBehaviour);
			collision.Run(animalA, animalB);
		}

		public void ClearBusyAnimals()
		{
			_busyAnimals.Clear();
		}
	}
}