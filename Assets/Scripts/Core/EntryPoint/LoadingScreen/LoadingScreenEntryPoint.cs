using Core.Services.Configs;
using Core.Services.Configs.Std;
using Core.Services.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils.Const;
using VContainer.Unity;

namespace Core.EntryPoint.LoadingScreen
{
	public class LoadingScreenEntryPoint : IStartable
	{
		private readonly ISceneProvider _sceneProvider;
		private readonly AnimalsConfigProvider _animalsConfigProvider;
		private readonly MovementsConfigProvider _movementsConfigProvider;

		public LoadingScreenEntryPoint(
			ISceneProvider sceneProvider,
			AnimalsConfigProvider animalsConfigProvider,
			MovementsConfigProvider movementsConfigProvider)
		{
			_sceneProvider = sceneProvider;
			_animalsConfigProvider = animalsConfigProvider;
			_movementsConfigProvider = movementsConfigProvider;
		}

		public async void Start()
		{
			await UniTask.WhenAll(
				_animalsConfigProvider.Init(),
				_movementsConfigProvider.Init(),
				_sceneProvider.LoadScene(SceneName.Main));

			await _sceneProvider.ShowScene(SceneName.Main);
		}
	}
}