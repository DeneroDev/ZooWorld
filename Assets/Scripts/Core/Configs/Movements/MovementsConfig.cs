using Core.Animals;
using Core.Movements;
using Project.Tools.DictionaryHelp;
using UnityEngine;

namespace Core.Configs.Movements
{
	[CreateAssetMenu(fileName = "MovementsConfig", menuName = "Configs/MovementsConfig", order = 10)]

	public class MovementsConfig : ScriptableObject
	{
		[SerializeField] public SerializableDictionary<MovementType, MovementConfig> Movements;
		
		public MovementConfig GetConfig(MovementType movementType)
		{
			return Movements[movementType];
		}
	}
}