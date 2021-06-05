using System;

namespace GameProject.Levels
{
	public class LevelAttribute : Attribute
	{
		public LevelAttribute(bool isLevel = true)
		{
			IsLevel = isLevel;
		}

		public bool IsLevel { get; }
	}
}