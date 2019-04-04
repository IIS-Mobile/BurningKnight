using System;
using BurningKnight.entity.level;
using BurningKnight.state;
using Lens.entity;
using Lens.input;

namespace BurningKnight.entity.item.use {
	public class DigUse : ItemUse {
		public void Use(Entity entity, Item item) {
			var cursor = Input.Mouse.GamePosition;
			var x = (int) Math.Floor(cursor.X / 16f);
			var y = (int) Math.Floor((cursor.Y + 8) / 16f);

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
				
				y = (int) Math.Floor((cursor.Y) / 16f);

				if (!level.IsInside(x, y)) {
					return;
				}
				
				index = level.ToIndex(x, y);
				
				if (level.Liquid[index] != 0) {
					level.Liquid[index] = 0;					
				} else {
					level.Set(index, Tile.Chasm);
				}
			} else {
				level.Set(index, Tile.FloorA);
			}
			
			level.UpdateTile(x, y);
			level.CreateBody(); // Fixme: super bad, cause body is huge. Think of body chunks 16x16 or so
		}
	}
}