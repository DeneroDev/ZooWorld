using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Animals
{
	public interface IAnimalsFactory
	{
		UniTask Initialize();
		Animal Create(AnimalType type, Vector3 position, Quaternion rotation);
		void Release(Animal animal);
	}
}
