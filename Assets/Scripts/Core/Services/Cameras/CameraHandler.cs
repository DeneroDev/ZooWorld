using UnityEngine;

namespace Core.Services.Cameras
{
	public class CameraHandler : ICameraHandler
	{
		public Camera Camera { get; private set; }
        
		public Rect Bounds { get; private set; }

		public void Initialize(Camera camera)
		{
			Camera = camera;
            
			CalculateBounds();
		}

		private void CalculateBounds()
		{
			Vector3 bottomLeft = Camera.ScreenToWorldPoint(new Vector3(0, 0, Camera.transform.position.y));
			Vector3 topRight = Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.transform.position.y));

			float minX = Mathf.Min(bottomLeft.x, topRight.x);
			float maxX = Mathf.Max(bottomLeft.x, topRight.x);
			float minZ = Mathf.Min(bottomLeft.z, topRight.z);
			float maxZ = Mathf.Max(bottomLeft.z, topRight.z);

			Bounds = new Rect(minX, minZ, maxX - minX, maxZ - minZ);
		}
	}
}