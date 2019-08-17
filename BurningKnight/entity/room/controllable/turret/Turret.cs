using System;
using BurningKnight.entity.component;
using BurningKnight.entity.projectile;
using BurningKnight.level;
using BurningKnight.physics;
using BurningKnight.util;
using Lens.entity;
using Lens.util;
using Lens.util.tween;
using Microsoft.Xna.Framework;
using VelcroPhysics.Dynamics;

namespace BurningKnight.entity.room.controllable.turret {
	public class Turret : RoomControllable, CollisionFilterEntity {
		private float beforeNextBullet;
		protected bool Rotates;

		protected uint Angle {
			get => GetComponent<AnimationComponent>().Animation.Frame;
			set => GetComponent<AnimationComponent>().Animation.Frame = value;
		}
		
		public override void AddComponents() {
			base.AddComponents();
			
			AddComponent(new RectBodyComponent(4, 4, 8, 6, BodyType.Static));

			var a = new AnimationComponent("turret");
			AddComponent(a);

			a.Animation.Tag = "single";
			a.Animation.Paused = true;
			
			AddComponent(new ShadowComponent());
		}

		public override void TurnOn() {
			base.TurnOn();
			beforeNextBullet = 0;
		}

		public override void Update(float dt) {
			base.Update(dt);
			beforeNextBullet -= dt;

			if (beforeNextBullet <= 0) {
				beforeNextBullet = 2;

				var a = GetComponent<AnimationComponent>();

				Tween.To(0.6f, a.Scale.X, x => a.Scale.X = x, 0.2f);
				Tween.To(1.6f, a.Scale.Y, x => a.Scale.Y = x, 0.2f).OnEnd = () => {

					Tween.To(1.8f, a.Scale.X, x => a.Scale.X = x, 0.1f);
					Tween.To(0.2f, a.Scale.Y, x => a.Scale.Y = x, 0.1f).OnEnd = () => {

						Tween.To(1, a.Scale.X, x => a.Scale.X = x, 0.4f);
						var t = Tween.To(1, a.Scale.Y, x => a.Scale.Y = x, 0.4f);

						if (Rotates) {
							t.OnEnd = () => { Angle++; };
						}

						Fire(Angle / 4f * Math.PI);
					};
				};
			}
		}

		protected virtual void Fire(double angle) {
			SendProjectile(angle);
		}

		protected void SendProjectile(double angle) {
			var projectile = Projectile.Make(this, "small", angle, 6f);

			projectile.Center += MathUtils.CreateVector(angle, 8f);
			projectile.AddLight(32f, Color.Red);

			AnimationUtil.Poof(projectile.Center);
		}

		public bool ShouldCollide(Entity entity) {
			return !(entity is Projectile || entity is Chasm);
		}
	}
}