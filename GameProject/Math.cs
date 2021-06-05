using System;
using System.Drawing;
using System.Numerics;

namespace GameProject
{
	public static class Math
	{
		public static Vector2 ConvertToRenderPos(Vector2 objPos, Vector2 playerPos, Vector2 playerSize,
			Vector2 controlSize)
		{
			return controlSize / 2 + objPos - playerSize / 2 - playerPos;
		}

		public static Vector2 ConvertToModelPos(Vector2 objPos, Vector2 playerPos, Vector2 playerSize,
			Vector2 controlSize)
		{
			return objPos - controlSize / 2 + playerSize / 2 + playerPos;
		}

		public static bool AreIntersected(RectangleF r0, RectangleF r1)
		{
			return MathF.Min(r0.Right, r1.Right) - MathF.Max(r0.Left, r1.Left) >= 0 &&
			       MathF.Min(r0.Bottom, r1.Bottom) - MathF.Max(r0.Top, r1.Top) >= 0;
		}
	}
}