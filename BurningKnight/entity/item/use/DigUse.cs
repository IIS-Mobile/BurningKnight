using System;
using BurningKnight.entity.component;
using BurningKnight.level.tile;
using BurningKnight.state;
using Lens.entity;
using Lens.input;

namespace BurningKnight.entity.item.use {
	public class DigUse : ItemUse {
		public override void Use(Entity entity, Item item) {
			var cursor = entity.GetComponent<CursorComponent>().Cursor.GamePosition;
			var x = (int) Math.Floor(cursor.X / 16f);
			var y = (int) Math.Floor((cursor.Y) / 16f);

			var level = Run.Level;

			if (!level.IsInside(x, y)) {
				return;
			}

			var index = level.ToIndex(x, y);
			var tile = level.Tiles[index];
			var t = (Tile) tile;
			
			if (!t.IsWall()) {
				if (t == Tile.Chasm) {
					return;
				}
								
				if (level.Liquid[index] != 0) {
					level.Liquid[index] = 0;
				} else {
					level.Liquid[index] = 0;
					level.Set(index, Tile.Chasm);
				}
			} else {
				level.Set(index, Tile.FloorA);
			}
			
			level.UpdateTile(x, y);
			level.CreateBody();
			level.CreateDestroyableBody();
			level.LoadPassable();
		}
	}
}