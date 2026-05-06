using System;
using Core.CollisionBehaviours;
using Core.Movements;

namespace Core.Configs.Animals
{
	[Serializable]
	public struct AnimalData
	{
		public CollisionBehaviourType collisionBehavior;
		public MovementType movement;
	}
}