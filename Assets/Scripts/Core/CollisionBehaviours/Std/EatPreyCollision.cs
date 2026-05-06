using Core.Animals;
using UnityEngine;
using VContainer;

namespace Core.CollisionBehaviours.Std
{
	public class EatPreyCollision : ICollisionHandlerStrategy
	{
		private readonly IObjectResolver _resolver;

		public EatPreyCollision(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		public void Run(Animal a, Animal b)
		{
			var factory = _resolver.Resolve<IAnimalsFactory>();
			if (a.CollisionBehaviour == CollisionBehaviourType.Prey)
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