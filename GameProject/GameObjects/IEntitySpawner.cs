using System.Collections.Generic;

namespace GameProject.GameObjects
{
	public interface IEntitySpawner
	{
		List<IEntity> CustomEntities { get; }
	}
}