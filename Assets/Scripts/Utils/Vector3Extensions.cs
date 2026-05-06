using UnityEngine;

namespace Utils
{
	public static class Vector3Extensions
	{
		public static Vector3 WithYZero(this Vector3 v)
		{
			var normalizeVector = new Vector3(v.x, 0, v.z);
			return v.sqrMagnitude > 0.0001f ? normalizeVector.normalized : Vector3.forward;
		}
	}
}