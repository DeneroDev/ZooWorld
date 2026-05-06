using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services.Assets
{
	public interface IResourcesProvider
	{
		UniTask<T> Load<T>(string id) where T : Object;
		UniTask<IList<T>> Load<T>(IEnumerable<string> ids) where T : Object;
	}
}