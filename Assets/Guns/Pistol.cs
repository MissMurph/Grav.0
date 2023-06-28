using UnityEngine.InputSystem;
using Grav.Players;
using Grav.Entities;

namespace Grav.Guns {

	public class Pistol : Gun {

		public override void Reload (InputAction.CallbackContext context) {
			if (context.performed) {
				Reload();
			}
		}

		public override void Trigger (InputAction.CallbackContext context) {
			if (context.performed) {
				if (Chambered) {
					FireProjectile(Player.Instance.Direction, 5f, OnHit, Player.Instance.gameObject);
					cooldownTimer -= fireDelay;
				}
				else if (CurrentAmmo <= 0) {
					Reload(context);
				}
			}
		}

		protected void OnHit (HitInfo hit) {
			if (hit.Result) {
				hit.Target.Attack(Damage);
			}

			Destroy(hit.Bullet.gameObject);
		}

		public override void Zoom (InputAction.CallbackContext context) {
			
		}
	}
}