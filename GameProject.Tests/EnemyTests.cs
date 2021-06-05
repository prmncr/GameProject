using System.Collections.Generic;
using System.Reflection;
using GameProject.GameObjects;
using GameProject.Levels;
using GameProject.Tests.TestLevels;
using NUnit.Framework;

namespace GameProject.Tests
{
	public class EnemyTests
	{
		[Test]
		public void EnemyCanMoveToPlayerWithoutWalls()
		{
			var map = new Game(typeof(TestLevel1));
			var enemies = LevelController.Enemies;
			Assert.IsTrue(enemies?[0].CheckPath().Item1);
		}

		[Test]
		public void EnemyCanNotMoveToPlayerThroughWalls()
		{
			var map = new Game(typeof(TestLevel2));
			var enemies = LevelController.Enemies;
			Assert.IsFalse(enemies?[0].CheckPath().Item1);
		}
	}
}