using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Guns {

	public class RocketLauncher : Gun {

		protected virtual void Awake () {
			ItemName = "Rocket Launcher";

			baseDamage = 50;
			baseFireRate = 1f;
			baseRecoil = 1f;
			baseAccuracy = 0.95f;
			baseMagSize = 1;
			baseReloadTime = 5f;

			parts.Add(new Action(0, this));
			parts.Add(new Barrel(0, this));
			parts.Add(new Magazine(0, this));
			parts.Add(new Trigger(0, this));

			fireDelay = 1f / FireRate;

			InitializeAmmo();
		}

		public override void LeftMouseButton () {
			FireGun(ItemParent.Direction);
		}

		protected override void FireGun (Vector2 direction) {
			if (_currentAmmo <= 0) { StartCoroutine(ReloadGun()); return; }
			if (isReloading || cooldownTick > 0) return;

			ItemParent.AddForce(-direction, Recoil * Damage);

			float angle = Mathf.Atan2(direction.y, direction.x);
			angle *= Mathf.Rad2Deg;
			float angleDiff = 360 * (1f - Accuracy);
			angle = (angle - (angleDiff / 2)) + (angleDiff * ((float)GameManager.RandomGenerator.Next(0, 100) / 100f));
			angle *= Mathf.Deg2Rad;

			direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

			FireProjectile(direction, angle, Damage, 5f, transform.position);

			_currentAmmo -= AmmoConsumption;

			OnShoot(this);
			GameManager.UpdateText(GameManager.UI_ActiveGun.CurrentAmmoCounter, CurrentAmmo.ToString());
			cooldownTick = fireDelay;
		}

		public virtual new Rocket FireProjectile (Vector2 direction, float angle, int damage, float speed, Vector2 position, params GameObject[] ignoreCollision) {
			Rocket b = Instantiate(GameManager.Resources.getPrefab("rocket"), position, Quaternion.Euler(0, 0, angle)).GetComponent<Rocket>();
			b.Initialize(damage, this, direction, speed, ignoreCollision);
			return b;
		}
	}
}