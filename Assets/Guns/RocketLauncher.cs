using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grav.Guns {

	public class RocketLauncher : Gun {
		public override void Trigger (InputAction.CallbackContext context) {
			/*if (_currentAmmo <= 0) { StartCoroutine(ReloadGun()); return; }
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
			cooldownTick = fireDelay;*/
		}

		public override void Zoom (InputAction.CallbackContext context) {
			throw new System.NotImplementedException();
		}

		public override void Reload (InputAction.CallbackContext context) {
			throw new System.NotImplementedException();
		}

		/*public virtual new Rocket FireProjectile (Vector2 direction, float angle, int damage, float speed, Vector2 position, params GameObject[] ignoreCollision) {
			Rocket b = Instantiate(GameManager.Resources.getPrefab("rocket"), position, Quaternion.Euler(0, 0, angle)).GetComponent<Rocket>();
			b.Initialize(damage, this, direction, speed, ignoreCollision);
			return b;
		}*/
	}
}