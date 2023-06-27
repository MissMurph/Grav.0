using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;
using UnityEngine.InputSystem;

namespace Grav.Guns {

	public class ShotGun : Gun {

		protected int shotCount;

		public override void Trigger (InputAction.CallbackContext context) {
			/*if (_currentAmmo <= 0) { StartCoroutine(ReloadGun()); return; }
			if (!isReloading && cooldownTick > 0) return;

			ItemParent.AddForce(-direction, Recoil * Damage * 2);

			float angle = Mathf.Atan2(direction.y, direction.x);
			angle *= Mathf.Rad2Deg;
			float angleDiff = 360 * (1f - Accuracy);

			for (int i = 0; i < shotCount; i++) {
				float newAngle = (angle - (angleDiff / 2)) + (angleDiff * ((float)GameManager.RandomGenerator.Next(0, 100) / 100f));
				newAngle *= Mathf.Deg2Rad;

				direction = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));

				FireRay(direction, Damage);
			}

			_currentAmmo -= AmmoConsumption;
			GameManager.UpdateText(GameManager.UI_ActiveGun.CurrentAmmoCounter, CurrentAmmo.ToString());
			cooldownTick = fireDelay;*/
		}

		public override void Zoom (InputAction.CallbackContext context) {
			throw new System.NotImplementedException();
		}

		public override void Reload (InputAction.CallbackContext context) {
			throw new System.NotImplementedException();
		}
	}
}