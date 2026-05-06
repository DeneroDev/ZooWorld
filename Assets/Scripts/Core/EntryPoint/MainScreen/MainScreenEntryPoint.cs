using Core.Animals;
using Core.Animals.Spawner;
using Core.CollisionBehaviours;
using Core.CollisionBehaviours.Observer;
using Core.Services.Cameras;
using UnityEngine;
using VContainer.Unity;

namespace Core.EntryPoint.MainScreen
{
	public class MainScreenEntryPoint : IStartable, ITickable, IPostFixedTickable
	{
		private readonly ICameraHandler _cameraHandler;
		private readonly Camera _camera;
		private readonly IAnimalsSpawner _animalsSpawner;
		private readonly IAnimalsFactory _animalsFactory;
		private readonly ICollisionsObserver _collisionsObserver;

		public MainScreenEntryPoint(ICameraHandler cameraHandler,
			Camera camera,
			IAnimalsSpawner animalsSpawner,
			IAnimalsFactory animalsFactory,
			ICollisionsObserver collisionsObserver)
		{
			_cameraHandler = cameraHandler;
			_camera = camera;
			_animalsSpawner = animalsSpawner;
			_animalsFactory = animalsFactory;
			_collisionsObserver = collisionsObserver;
		}
        
		public async void Start()
		{
			_cameraHandler.Initialize(_camera);
			await _animalsFactory.Initialize();
		}

		public void Tick()
		{
			_animalsSpawner.Tick();
		}

		public void PostFixedTick()
		{
			_collisionsObserver.ClearBusyAnimals();
		}
	}
}