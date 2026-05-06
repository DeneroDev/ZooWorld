using Core.Animals;
using Core.Animals.Spawner;
using Core.Animals.Strategy;
using Core.CollisionBehaviours;
using Core.CollisionBehaviours.Observer;
using Core.Services.Cameras;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.EntryPoint.MainScreen
{
	public class MainScreenScope : LifetimeScope
	{
		[SerializeField] private Camera camera;
		
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ICameraHandler, CameraHandler>(Lifetime.Scoped);
			builder.Register<IStrategyResolver, StrategyResolver>(Lifetime.Scoped);
			builder.Register<ICollisionsObserver, CollisionsObserver>(Lifetime.Scoped);
			builder.Register<IAnimalsFactory, AnimalsFactory>(Lifetime.Scoped);
			builder.Register<IAnimalsSpawner, AnimalsSpawner>(Lifetime.Scoped);
			builder.RegisterInstance(camera);
			
			builder.RegisterEntryPoint<MainScreenEntryPoint>();
		}
	}
}
