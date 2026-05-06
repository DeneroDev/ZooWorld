using Core.Configs.Movements;
using Core.Services.Assets;
using Cysharp.Threading.Tasks;

namespace Core.Services.Configs.Std
{
	public class MovementsConfigProvider : IConfigsProvider
	{
		private readonly IResourcesProvider _resourceProvider;
		private MovementsConfig _config;
		
		public MovementsConfigProvider(IResourcesProvider resourcesProvider)
		{
			_resourceProvider = resourcesProvider;
		}

		public string ConfigPath => "MovementsConfig";

		public async UniTask Init()
		{
			_config = await _resourceProvider.Load<MovementsConfig>(ConfigPath);
		}
		
		public MovementsConfig GetConfig() => _config;
	}
}