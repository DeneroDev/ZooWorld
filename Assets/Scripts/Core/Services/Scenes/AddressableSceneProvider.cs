using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core.Services.Scenes
{
	public class AddressableSceneProvider : ISceneProvider
	{
		private Dictionary<string, SceneInstance> _cachedScenes = new();
		
		public async UniTask LoadScene(string sceneName)
		{
			if (IsCurrentScene(sceneName))
			{
				await UniTask.CompletedTask;
				return;
			}
			
			var handle = Addressables.LoadSceneAsync(sceneName);
			await handle;

			if (handle.Status != AsyncOperationStatus.Succeeded)
			{
				Addressables.Release(handle);
				Debug.LogError($"Failed to load scene '{sceneName}'. Status: {handle.Status}");
				return;
			}
			
			_cachedScenes.Add(sceneName, handle.Result);
			Addressables.Release(handle);
		}

		public async UniTask ShowScene(string sceneName)
		{
			if (IsCurrentScene(sceneName))
			{
				await UniTask.CompletedTask;
				return;
			}

			if (_cachedScenes.TryGetValue(sceneName, out var instance))
			{
				await instance.ActivateAsync();
				return;
			}
			
			await LoadScene(sceneName);
			await ShowScene(sceneName);
		}

		private bool IsCurrentScene(string sceneName)
		{
			return SceneManager.GetActiveScene().name == sceneName;
		}
	}
}