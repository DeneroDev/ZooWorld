using UnityEngine;

namespace Core.Movements.Std
{
	public class LinearMovement : IMovementStrategy
	{
		private readonly float _speed;

		public LinearMovement(float speed)
		{
			_speed = speed;
		}

		public void Tick(Rigidbody rb, float dt, ref Vector3 direction)
		{
			rb.linearVelocity = direction * _speed;
		}
	}
}