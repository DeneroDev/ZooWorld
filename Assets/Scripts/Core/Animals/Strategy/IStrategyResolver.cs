using Core.CollisionBehaviours;
using Core.Configs.Movements;
using Core.Movements;

namespace Core.Animals.Strategy
{
	public interface IStrategyResolver
	{
		IMovementStrategy ResolveMovement(MovementType type, in MovementConfig config);
		ICollisionHandlerStrategy ResolveCollision(CollisionBehaviourType animal1, CollisionBehaviourType animal2);
	}
}