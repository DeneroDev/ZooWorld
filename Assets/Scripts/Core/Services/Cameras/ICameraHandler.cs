using UnityEngine;

namespace Core.Services.Cameras
{
	public interface ICameraHandler
	{
		Camera Camera { get; }
		Rect Bounds { get; }
		void Initialize(Camera camera);
	}
}