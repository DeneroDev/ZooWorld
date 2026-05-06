using UnityEngine;

namespace Core.Movements
{
	public interface IMovementStrategy
	{
		void Tick(Rigidbody rb, float dt, ref Vector3 direction);
	}
}