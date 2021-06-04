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
	}
}