using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;
using Grav.Entities;
using UnityEngine.InputSystem;

namespace Grav.Guns {

	public class FlameThrower : Gun {
		[SerializeField]
		public int SplitCount { get; private set; }     //How many projectiles will be created on hit

		[SerializeField]
		protected int spreadChain;      //How many times will projectiles split

		public override void Trigger (InputAction.CallbackContext context) {
			/*if (_currentAmmo <= 0) { StartCoroutine(ReloadGun()); return; }
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
			cooldownTick = fireDelay;*/
		}

		public override void Zoom (InputAction.CallbackContext context) {
			throw new System.NotImplementedException();
		}

		public override void Reload (InputAction.CallbackContext context) {
			throw new System.NotImplementedException();
		}

		/*protected virtual void ProjectileHit (Entity e, FlameBullet b) {
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
		}*/

		/*public virtual FlameBullet FireProjectile (Vector2 direction, float angle, int damage, float speed, Vector2 position, int spreadChain, params GameObject[] ignoreCollision) {
			FlameBullet b = Instantiate(GameManager.Resources.getPrefab("flamebullet"), position, Quaternion.Euler(0, 0, angle)).GetComponent<FlameBullet>();
			b.Initialize(damage, this, direction, speed, spreadChain, ignoreCollision);
			return b;
		}*/
	}
}