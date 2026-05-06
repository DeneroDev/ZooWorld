using System;
using Core.CollisionBehaviours;

namespace Core.Animals.Spawner
{
	public interface IAnimalsSpawner
	{
		void Tick();
		event Action<CollisionBehaviourType> OnAnimalDied;
	}
}