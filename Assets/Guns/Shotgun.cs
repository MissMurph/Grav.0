using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Grav.Entities;
using System;
using Grav.Events;
using Grav.Players;

namespace Grav.Guns {

	public class Shotgun : Gun {

		[SerializeField]
		protected int shotCount;

		protected bool interruptReload;

		//Subscribing to the AmmoChange event to listen out for this guns own ammo change
		//By listening we can canel reloading half way through
		protected void Start () {
			interruptReload = false;
			EventBus.AddListener<AmmoChange>(OnAmmoChange);
		}

		public override void Trigger (InputAction.CallbackContext context) {
			if (context.performed) {
				if (isReloading) interruptReload = true;
				if (Chambered) {
					FireSpread(Player.Instance.Direction, 5f, shotCount, OnHit, Player.Instance.gameObject);
					cooldownTimer -= fireDelay;
				}
				else if (CurrentAmmo <= 0) {
					Reload(context);
				}
			}
		}

		public override void Zoom (InputAction.CallbackContext context) {
			
		}

		public override void Reload (InputAction.CallbackContext context) {
			if (context.performed) {
				Reload();
			}
		}

		protected virtual Projectile[] FireSpread (Vector3 direction, float speed, int count, Action<HitInfo> callback, params GameObject[] ignoreCollision) {
			System.Random rando = new();

			Projectile[] bullets = new Projectile[count];

			float angle = Mathf.Atan2(direction.y, direction.x);
			angle *= Mathf.Rad2Deg;
			float angleDiff = 360 * (1f - Accuracy);

			for (int i = 0; i < count; i++) {
				float newAngle = (angle - (angleDiff / 2)) + (angleDiff * (rando.Next(0, 100) / 100f));
				newAngle *= Mathf.Deg2Rad;

				Vector3 bulletDir = new Vector3(Mathf.Cos(newAngle), 0, Mathf.Sin(newAngle));
				bullets[i] = FireProjectile(bulletDir, speed, callback, ignoreCollision);
			}

			return bullets;
		}

		//This overrides the method to reload only one bullet at a time and post a seperate event for each
		//Fire while reloading to interrupt the reload
		protected override IEnumerator ReloadGun () {
			if (CurrentAmmo == MagSize) yield break;

			while (CurrentAmmo < MagSize) {
				AmmoChange _event = EventBus.Post(new AmmoChange(this, 1));
				if (_event.Canceled) yield break;

				isReloading = true;
				yield return new WaitForSeconds(ReloadTime);

				CurrentAmmo++;
				_event.Phase = Phase.Post;
				isReloading = false;

				EventBus.Post(_event);
			}
		}

		protected void OnHit (HitInfo hit) {
			if (hit.Result) {
				hit.Target.Attack(Damage);
			}

			Destroy(hit.Bullet.gameObject);
		}

		protected void OnAmmoChange (AmmoChange _event) {
			if (ReferenceEquals(_event.Target, this) && interruptReload && _event.Phase == Phase.Pre) {
				_event.Canceled = true;
			}
		}
	}
}