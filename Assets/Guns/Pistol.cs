using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;
using UnityEngine.InputSystem;
using Grav.Players;
using Grav.Entities;

namespace Grav.Guns {

	public class Pistol : Gun {
		public override void Reload (InputAction.CallbackContext context) {
			
		}

		public override void Trigger (InputAction.CallbackContext context) {
			if (context.performed) {
				FireProjectile(Player.Instance.Direction, 5f, OnHit, Player.Instance.gameObject);
			}
		}

		protected void OnHit (bool result, Entity hit) {
			if (result) {
				hit.Attack(Damage);
			}
		}

		public override void Zoom (InputAction.CallbackContext context) {
			
		}
	}
}