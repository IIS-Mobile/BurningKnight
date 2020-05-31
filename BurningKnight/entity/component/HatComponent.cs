using BurningKnight.assets.items;
using BurningKnight.assets.lighting;
using BurningKnight.entity.creature.player;
using BurningKnight.entity.item;
using BurningKnight.save;
using Lens.util.file;
using Microsoft.Xna.Framework;

namespace BurningKnight.entity.component {
	public class HatComponent : ItemComponent {
		public bool DoNotRender;
		
		public override void PostInit() {
			base.PostInit();
			
			if (Item == null) {
				var hat = GlobalSave.GetString("hat");

				if (hat != null) {
					Set(Items.CreateAndAdd(hat, Entity.Area), false);
				} else {
					Set(Items.CreateAndAdd("bk:no_hat", Entity.Area), false);
				}
			}
		}

		protected override bool ShouldReplace(Item item) {
			return item.Type == ItemType.Hat;
		}
		
		public override void Set(Item item, bool animate = true) {
			base.Set(item, animate);
			GlobalSave.Put("hat", item?.Id);

			if (item != null) {
				DoNotRender = item.Id == "bk:no_hat";

				if (Entity.HasComponent<LightComponent>()) {
					Entity.GetComponent<LightComponent>().Light.Color =
						item.Id == "bk:glowing_mushroom" ? new Color(0.05f, 0.4f, 1f, 1f) : Player.LightColor;
				}
			}
		}

		protected override void OnItemSet(Item previous) {
			base.OnItemSet(previous);
			
			foreach (var i in Entity.Area.Tagged[Tags.Item]) {
				if (i is EmeraldStand st) {
					st.RecalculatePrice();
				}
			}
		}
	}
}