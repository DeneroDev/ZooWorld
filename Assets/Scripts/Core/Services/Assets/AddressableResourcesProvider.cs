using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Services.Assets
{
	public class AddressableResourcesProvider : IResourcesProvider
	{
		public async UniTask<T> Load<T>(string id) where T : Object
		{
			var handle = Addressables.LoadAssetAsync<T>(id);
			var result = await handle.ToUniTask();

			if (handle.Status != AsyncOperationStatus.Succeeded)
			{
				Debug.LogError($"Failed to load id: {id}");
				Addressables.Release(handle);
				return null;
			}

			Addressables.Release(handle);
			return result;
		}

		public async UniTask<IList<T>> Load<T>(IEnumerable<string> ids) where T : Object
		{
			var handle = Addressables.LoadAssetsAsync<Object>(ids,
				null,
				Addressables.MergeMode.Union,
				false);

			var results = await handle.ToUniTask();

			if (handle.Status != AsyncOperationStatus.Succeeded)
			{
				Debug.LogError($"Failed to load ids: {ids}");
				Addressables.Release(handle);
				return null;
			}

			Addressables.Release(handle);

			var resultsList = new List<T>();
			foreach (var result in results)
			{
				if (result is T t)
				{
					resultsList.Add(t);
					continue;
				}

				if (result is not GameObject go)
					continue;

				resultsList.Add(go.GetComponent<T>());
			}

			return resultsList;
		}
	}
}