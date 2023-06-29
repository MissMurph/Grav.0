using Grav.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.UI.CanvasScaler;

namespace Grav.Guns {

	public class RocketLauncher : Gun {
		private Projectile activeRocket;

		public override void Trigger (InputAction.CallbackContext context) {
			if (context.performed) {
				if (Chambered && activeRocket == null) {
					Projectile rocket = FireProjectile(Player.Instance.Direction, 5f, OnHit, Player.Instance.gameObject);
					activeRocket = rocket;
					cooldownTimer -= fireDelay;
				}
				else if (CurrentAmmo <= 0) {
					Reload(context);
				}
			}
		}

		protected override void Update () {
			base.Update();

			if (activeRocket != null) {
				Vector3 worldPos = Player.ViewPort.ScreenToWorldPoint(new Vector3(Player.MousePosition.x, Player.MousePosition.y, 15));
				Vector3 mouseDir = new Vector3(worldPos.x - transform.position.x, 0, worldPos.z - transform.position.z).normalized;
				activeRocket.Direction = mouseDir;
			}
		}

		public override void Zoom (InputAction.CallbackContext context) {
			
		}

		public override void Reload (InputAction.CallbackContext context) {
			if (context.performed) {
				Reload();
			}
		}

		protected virtual void OnHit (HitInfo hit) {
			if (hit.Result) {
				hit.Target.Attack(Damage);
			}

			activeRocket = null;
			Destroy(hit.Bullet.gameObject);
		}
	}
}