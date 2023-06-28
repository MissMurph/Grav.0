using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;
using UnityEngine.InputSystem;
using Grav.Players;
using static UnityEngine.UI.CanvasScaler;
using Grav.Entities;
using Grav.Events;

namespace Grav.Guns {

	public class AssaultRifle : Gun {
		private bool firing = false;

		protected override void Update () {
			base.Update();

			if (firing) {
				if (Chambered) {
					FireProjectile(Player.Instance.Direction, 5f, OnHit, Player.Instance.gameObject);
					cooldownTimer -= fireDelay;
				}
				else if (CurrentAmmo <= 0) {
					Reload();
				}
			}
		}

        public override void Reload (InputAction.CallbackContext context) {
			if (context.performed) {
				Reload();
			}
		}

		public override void Trigger (InputAction.CallbackContext context) {
			if (context.performed) firing = true;
			if (context.canceled) firing = false;
		}

		public override void Zoom (InputAction.CallbackContext context) {
			
		}

		protected void OnHit (HitInfo hit) {
			if (hit.Result) {
				hit.Target.Attack(Damage);
			}

			Destroy(hit.Bullet.gameObject);
		}
	}
}