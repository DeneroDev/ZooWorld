using Core.Animals;
using UnityEngine;
using VContainer;

namespace Core.CollisionBehaviours.Std
{
	public class EatRandomCollision : ICollisionHandlerStrategy
	{
		private readonly IObjectResolver _resolver;

		public EatRandomCollision(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		public void Run(Animal a, Animal b)
		{
			var factory = _resolver.Resolve<IAnimalsFactory>();
			if (Random.Range(0, 2) == 0)
			{
				factory.Release(a);
				b.ShowTastySign();
			}
			else
			{
				factory.Release(b);
				a.ShowTastySign();
			}
		}
	}
}