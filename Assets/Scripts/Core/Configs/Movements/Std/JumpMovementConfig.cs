using UnityEngine;

namespace Core.Configs.Movements.Std
{
	[CreateAssetMenu(fileName = "JumpMovementConfig", menuName = "MovementConfigs/JumpMovementConfig", order = 10)]
	public class JumpMovementConfig : MovementConfig
	{
		public float JumpForce;
		public float JumpUpForce;
		public float JumpCooldown;
	}
}