using BurningKnight.level.rooms;
using BurningKnight.level.tile;
using BurningKnight.util.geometry;
using Lens.util.math;

namespace BurningKnight.level.floors {
	public class GeometryFloor : FloorPainter {
		public override void Paint(Level level, RoomDef room, Rect inside, bool gold) {
			Painter.Fill(level, inside, Tiles.RandomFloor(gold));

			if (Random.Chance()) {
				Painter.FillEllipse(level, inside, gold && Random.Chance(30) ? Tile.FloorD : Tiles.RandomNewFloor());
			}

			if (Random.Chance(20)) {
				return;
			}
			
			inside = inside.Shrink(Random.Int(1, 3));

			if (Random.Chance()) {
				Painter.Fill(level, inside, gold ? Tile.FloorD : Tiles.RandomNewFloor());
			} else {
				Painter.Fill(level, inside, gold ? Tile.FloorD : Tiles.RandomNewFloor());
			}
			
			if (Random.Chance(40)) {
				return;
			}
			
			inside = inside.Shrink(Random.Int(1, 3));

			if (Random.Chance()) {
				Painter.Fill(level, inside, gold && Random.Chance(30) ? Tile.FloorD : Tiles.RandomNewFloor());
			} else {
				Painter.Fill(level, inside, gold && Random.Chance(30) ? Tile.FloorD : Tiles.RandomNewFloor());
			}
		}
	}
}