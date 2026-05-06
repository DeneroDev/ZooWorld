using Core.Configs.Animals;
using UnityEngine;
using Utils;

namespace Core.Animals.Strategy
{
	public class TurnBackHandler
	{
		private readonly AnimalsConfig _animalsConfig;

		public TurnBackHandler(AnimalsConfig config)
		{
			_animalsConfig = config;
		}

		public void Tick(Rigidbody rigidbody, Rect bounds, ref Vector3 direction)
		{
			var position = rigidbody.position;

			var inner = bounds.InsetXZ(_animalsConfig.TurnBackBoundsInset);
			var outX = position.x < inner.xMin || position.x > inner.xMax;
			var outZ = position.z < inner.yMin || position.z > inner.yMax;

			if (!outX && !outZ) return;

			var newDirection = -direction.WithYZero();

			var randomAngle = Random.Range(-_animalsConfig.RandomRotationRange, _animalsConfig.RandomRotationRange);
			newDirection = Quaternion.Euler(0f, randomAngle, 0f) * newDirection;
			direction = newDirection.WithYZero();

			var speed = rigidbody.linearVelocity.magnitude;
			rigidbody.linearVelocity = newDirection * speed;
		}
	}
}