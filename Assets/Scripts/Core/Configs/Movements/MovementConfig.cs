using UnityEngine;

namespace Core.Configs.Movements
{
	[CreateAssetMenu(fileName = "DefaultMovementConfig", menuName = "MovementConfigs/DefaultMovementConfig", order = 10)]
	public class MovementConfig : ScriptableObject
	{
		public float Speed;
	}
}