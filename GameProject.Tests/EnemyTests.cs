using System.Collections.Generic;
using System.Reflection;
using GameProject.GameControl;
using GameProject.GameObjects;
using GameProject.Tests.TestLevels;
using NUnit.Framework;

namespace GameProject.Tests
{
	public class EnemyTests
	{
		[Test]
		public void CheckPathTest1()
		{
			var map = new GameMap(new TestLevel1(), out _);
			var field = typeof(GameMap).GetField("_enemies", BindingFlags.NonPublic | BindingFlags.Instance);
			var enemy = (field?.GetValue(map) as List<Enemy>)?[0];
			Assert.IsTrue(enemy?.CheckPath().Item1);
		}
		
		[Test]
		public void CheckPathTest2()
		{
			var map = new GameMap(new TestLevel2(), out _);
			var field = typeof(GameMap).GetField("_enemies", BindingFlags.NonPublic | BindingFlags.Instance);
			var enemy = (field?.GetValue(map) as List<Enemy>)?[0];
			Assert.IsFalse(enemy?.CheckPath().Item1);
		}
	}
}