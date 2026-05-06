using Core.CollisionBehaviours;
using Core.CollisionBehaviours.Std;
using Core.Configs.Movements;
using Core.Configs.Movements.Std;
using Core.Movements;
using Core.Movements.Std;
using Core.Services.Configs;
using VContainer;

namespace Core.Animals.Strategy
{
	public class StrategyResolver : IStrategyResolver
	{
		private readonly BounceCollision _bounce;
		private readonly EatRandomCollision _eatRandom;
		private readonly EatPreyCollision _eatPrey;
		
		public StrategyResolver(AnimalsConfigProvider animalsConfigProvider, IObjectResolver objectResolver)
		{
			_bounce = new BounceCollision(animalsConfigProvider);
			_eatRandom = new EatRandomCollision(objectResolver);
			_eatPrey = new EatPreyCollision(objectResolver);
		}
		
		public IMovementStrategy ResolveMovement(MovementType type, in MovementConfig parameters) => type switch
		{
			MovementType.Linear => new LinearMovement(parameters.Speed),
			MovementType.Jump => new JumpMovement(((JumpMovementConfig)parameters).JumpForce, ((JumpMovementConfig)parameters).JumpUpForce, ((JumpMovementConfig)parameters).JumpCooldown),
			_ => new LinearMovement(parameters.Speed)
		};
		
		public ICollisionHandlerStrategy ResolveCollision(CollisionBehaviourType animal1, CollisionBehaviourType animal2)
		{
			if (animal1 == CollisionBehaviourType.Predator && animal2 == CollisionBehaviourType.Predator)
				return _eatRandom;
			
			if(animal1 == CollisionBehaviourType.Predator || animal2 == CollisionBehaviourType.Predator)
				return _eatPrey;

			return _bounce;
		}
	}
}