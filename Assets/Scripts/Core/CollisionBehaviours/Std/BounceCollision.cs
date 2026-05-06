using Core.Animals;
using Core.Configs.Animals;
using Core.Services.Configs;
using UnityEngine;
using Utils;

namespace Core.CollisionBehaviours.Std
{
	public class BounceCollision : ICollisionHandlerStrategy
	{
		private readonly AnimalsConfig _animalsConfigProvider;

		public BounceCollision(AnimalsConfigProvider animalsConfigProvider)
		{
			_animalsConfigProvider = animalsConfigProvider.GetConfig();
		}

		public void Run(Animal a, Animal b)
		{
			var rbA = a.Rigidbody;
			var rbB = b.Rigidbody;

			var normal = rbB.position - rbA.position;
			normal.y = 0f;

			if (normal.sqrMagnitude < _animalsConfigProvider.MagnitudeThreshold)
			{
				normal = rbB.linearVelocity - rbB.linearVelocity;
				normal.y = 0f;

				if (normal.sqrMagnitude < _animalsConfigProvider.MagnitudeThreshold) normal = Vector3.forward;
			}

			normal.Normalize();

			var velocityA = rbB.linearVelocity;
			var velocityB = rbB.linearVelocity;

			rbA.linearVelocity = Vector3.Reflect(velocityA, normal);
			rbB.linearVelocity = Vector3.Reflect(velocityB, -normal);

			a.Direction = rbA.linearVelocity.WithYZero();
			b.Direction = rbB.linearVelocity.WithYZero();
		}
	}
}