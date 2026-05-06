using Core.Animals;

namespace Core.CollisionBehaviours
{
	public interface ICollisionHandlerStrategy
	{
		void Run(Animal a, Animal b);
	}
}