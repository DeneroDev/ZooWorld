using Cysharp.Threading.Tasks;

namespace Core.Services.Scenes
{
	public interface ISceneProvider
	{
		UniTask LoadScene(string sceneName);
		UniTask ShowScene(string sceneName);
	}
}