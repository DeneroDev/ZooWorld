using UnityEngine;

namespace Core.Movements.Std
{
	public class JumpMovement : IMovementStrategy
	{
		private readonly float _jumpForce;
		private readonly float _jumpUpForce;
		private readonly float _cooldown;

		private float _timer;

		public JumpMovement(float jumpForce, float jumpUpForce, float cooldown)
		{
			_jumpForce = jumpForce;
			_jumpUpForce = jumpUpForce;
			_cooldown = cooldown;
		}

		public void Tick(Rigidbody rb, float dt, ref Vector3 direction)
		{
			_timer -= dt;

			if (!(_timer <= 0f) || !IsGrounded(rb)) 
				return;
			
			_timer = _cooldown;
			direction.y = 0f;
			direction.Normalize();

			var jumpVector = direction * _jumpForce + Vector3.up * _jumpUpForce;
			rb.linearVelocity = Vector3.zero;
			rb.AddForce(jumpVector, ForceMode.Impulse);
		}

		private bool IsGrounded(Rigidbody rb)
		{
			var origin = rb.position - Vector3.up * 0.1f;
			return Physics.Raycast(origin, Vector3.down, 0.5f);
		}
	}
}