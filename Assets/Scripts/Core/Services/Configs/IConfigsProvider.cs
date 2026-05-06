using Cysharp.Threading.Tasks;

namespace Core.Services.Configs
{
	public interface IConfigsProvider
	{
		public string ConfigPath { get; }
		UniTask Init();
	}
}