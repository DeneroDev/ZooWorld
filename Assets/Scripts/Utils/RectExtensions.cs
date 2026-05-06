using UnityEngine;

namespace Utils
{
	public static class RectExtensions
	{
		public static Rect InsetXZ(this Rect worldXZBounds, float rawInset)
		{
			var halfW = worldXZBounds.width * 0.5f;
			var halfH = worldXZBounds.height * 0.5f;
			var inset = Mathf.Min(rawInset, Mathf.Max(0f, Mathf.Min(halfW, halfH) - 0.0001f));
			return new Rect(
				worldXZBounds.x + inset,
				worldXZBounds.y + inset,
				worldXZBounds.width - 2f * inset,
				worldXZBounds.height - 2f * inset);
		}
	}
}