using System.Collections.Generic;
using Core.Animals;
using Project.Tools.DictionaryHelp;
using UnityEngine;

namespace Core.Configs.Animals
{
	[CreateAssetMenu(fileName = "AnimalsConfig", menuName = "Configs/AnimalsConfig", order = 10)]
	public class AnimalsConfig : ScriptableObject
	{
		[SerializeField] private float animalSpawnInterval;
		[SerializeField] private float spawnGroundHeight;
		[SerializeField] private int maxAnimalsOnLevel;
		[SerializeField] private int poolPrewarmPerType;
		[SerializeField] private float randomRotationRange;
		[SerializeField] private float turnBackBoundsInset;
		[SerializeField] private float tastyLabelDuration;
		[SerializeField] private float collisionCheckInterval;
		[SerializeField] private float magnitudeThreshold;
		[SerializeField] private float animalRotationSpeed;
		[SerializeField] private SerializableDictionary<AnimalType, AnimalData> animalsData;
		[SerializeField] private SerializableDictionary<AnimalType, string> animalIds;
        
		public float AnimalSpawnInterval => animalSpawnInterval;
		public float SpawnGroundHeight => spawnGroundHeight;
		public int MaxAnimalsOnLevel => maxAnimalsOnLevel;
		public int PoolPrewarmPerType => poolPrewarmPerType;
		public float RandomRotationRange => randomRotationRange;
		public float TurnBackBoundsInset => turnBackBoundsInset;
		public float TastyLabelDuration => tastyLabelDuration;
		public float CollisionCheckInterval => collisionCheckInterval;
		public float MagnitudeThreshold => magnitudeThreshold;
		public float AnimalRotationSpeed => animalRotationSpeed;
		public AnimalData GetAnimalData(AnimalType animalType) => animalsData[animalType];

		public List<string> GetAnimalIds()
		{
			var result = new List<string>(animalIds.Count);
			foreach (var id in animalIds)
			{
				result.Add(id.Value);
			}

			return result;
		}
	}
}