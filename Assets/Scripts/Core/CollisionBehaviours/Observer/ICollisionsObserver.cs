using Core.Animals;

namespace Core.CollisionBehaviours.Observer
{
	public interface ICollisionsObserver
	{
		void Register(Animal animal);
		void Unregister(Animal animal);
		void ClearBusyAnimals();
	}
}