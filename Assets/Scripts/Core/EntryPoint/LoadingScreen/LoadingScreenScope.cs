using Core.Services.Assets;
using Core.Services.Configs;
using Core.Services.Configs.Std;
using Core.Services.Scenes;
using VContainer;
using VContainer.Unity;

namespace Core.EntryPoint.LoadingScreen
{
	public class LoadingScreenScope : LifetimeScope
	{
		protected override void Awake()
		{
			DontDestroyOnLoad(gameObject);
			base.Awake();
		}

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<IResourcesProvider, AddressableResourcesProvider>(Lifetime.Singleton);
			builder.Register<ISceneProvider, AddressableSceneProvider>(Lifetime.Singleton);
			builder.Register<AnimalsConfigProvider>(Lifetime.Singleton);
			builder.Register<MovementsConfigProvider>(Lifetime.Singleton);

			builder.RegisterEntryPoint<LoadingScreenEntryPoint>();
		}
	}
}
