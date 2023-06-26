using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;
using Grav.Entities;

namespace Grav.Guns {

	public class FlameThrower : Gun {
		public int SplitCount { get; private set; }     //How many projectiles will be created on hit

		protected int spreadChain;      //How many times will projectiles split

		protected virtual void Awake () {
			ItemName = "Flame Thrower";

			baseDamage = 10;
			baseFireRate = 2f;
			baseRecoil = 1f;
			baseAccuracy = 0.95f;
			baseMagSize = 4;
			baseReloadTime = 5f;

			SplitCount = 3;
			spreadChain = 3;

			parts.Add(new Action(0, this));
			parts.Add(new Barrel(0, this));
			parts.Add(new Stock(0, this));
			parts.Add(new Magazine(0, this));
			parts.Add(new Trigger(0, this));

			fireDelay = 1f / FireRate;

			InitializeAmmo();
		}

		public override void LeftMouseButtonDown () {
			FireGun(ItemParent.Direction);
		}

		protected override void FireGun (Vector2 direction) {
			if (_currentAmmo <= 0) { StartCoroutine(ReloadGun()); return; }
			if (!isReloading && cooldownTick > 0) return;

			ItemParent.AddForce(-direction, Recoil * Damage);

			float angle = Mathf.Atan2(direction.y, direction.x);
			angle *= Mathf.Rad2Deg;
			float angleDiff = 360 * (1f - Accuracy);
			float newAngle = (angle - (angleDiff / 2)) + (angleDiff * ((float)GameManager.RandomGenerator.Next(0, 100) / 100f));

			direction = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

			FireProjectile(direction, newAngle, Damage, 5f, transform.position, spreadChain);

			_currentAmmo -= AmmoConsumption;
			GameManager.UpdateText(GameManager.UI_ActiveGun.CurrentAmmoCounter, CurrentAmmo.ToString());
			cooldownTick = fireDelay;
		}

		public virtual void ProjectileHit (Entity e, FlameBullet b) {
			base.ProjectileHit(e, b);

			if (b.SpreadChain <= 0) return;

			Vector2 direction = b.Direction;

			float angle = Mathf.Atan2(direction.y, direction.x);
			angle *= Mathf.Rad2Deg;
			float angleDiff = 360 * (1f - Accuracy - 0.15f);

			for (int i = 0; i < SplitCount; i++) {
				float newAngle = (angle - (angleDiff / 2)) + ((angleDiff / SplitCount) * i);

				Vector2 newDirection = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

				FireProjectile(newDirection, newAngle, Damage, 5f, b.transform.position, b.SpreadChain - 1, e.gameObject);
			}
		}

		public virtual FlameBullet FireProjectile (Vector2 direction, float angle, int damage, float speed, Vector2 position, int spreadChain, params GameObject[] ignoreCollision) {
			FlameBullet b = Instantiate(GameManager.Resources.getPrefab("flamebullet"), position, Quaternion.Euler(0, 0, angle)).GetComponent<FlameBullet>();
			b.Initialize(damage, this, direction, speed, spreadChain, ignoreCollision);
			return b;
		}
	}
}