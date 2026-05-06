using Core.Configs.Animals;
using Core.Services.Assets;
using Cysharp.Threading.Tasks;

namespace Core.Services.Configs
{
	public class AnimalsConfigProvider : IConfigsProvider
	{
		private readonly IResourcesProvider _resourceProvider;
		private AnimalsConfig _config;
		
		public AnimalsConfigProvider(IResourcesProvider resourcesProvider)
		{
			_resourceProvider = resourcesProvider;
		}

		public string ConfigPath => "AnimalsConfig";

		public async UniTask Init()
		{
			_config = await _resourceProvider.Load<AnimalsConfig>(ConfigPath);
		}
		
		public AnimalsConfig GetConfig() => _config;
	}
}