using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;
using Grav.Entities;
using UnityEngine.InputSystem;
using System;
using Grav.Players;

namespace Grav.Guns {

	public class FlameThrower : Gun {

		[SerializeField]
		public int SplitCount;   //How many projectiles will be created on hit

		[SerializeField]
		protected int ChainLength;      //How many times will projectiles split

		private Dictionary<Projectile, int> activeChains;

		protected override void Awake () {
			base.Awake();

			activeChains = new Dictionary<Projectile, int>();
		}

		public override void Trigger (InputAction.CallbackContext context) {
			if (context.performed) {
				if (Chambered) {
					Projectile chainStart = FireProjectile(Player.Instance.Direction, 5f, OnHit, Player.Instance.gameObject);
					activeChains.Add(chainStart, ChainLength);
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

		protected virtual void OnHit (HitInfo result) {
			if (result.Result) {
				if (activeChains.TryGetValue(result.Bullet, out int chainLength)) {
					Projectile[] newBullets = FireSpread(result.Target.transform.position, result.Bullet.Direction, 5f, SplitCount, OnHit, result.Target.gameObject);

					//Only if there's more chains remaining are we adding it to activeChains. If there's no more remaining there's no need to track
					if (chainLength - 1 > 0) {
						foreach (Projectile bullet in newBullets) {
							activeChains.Add(bullet, chainLength - 1);
						}
					}

					activeChains.Remove(result.Bullet);
				}
			}

			Destroy(result.Bullet.gameObject);
		}

		protected virtual Projectile[] FireSpread (Vector3 position, Vector3 direction, float speed, int count, Action<HitInfo> callback, params GameObject[] ignoreCollision) {
			System.Random rando = new();

			Projectile[] bullets = new Projectile[count];

			float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
			float angleDiff = 360 * (1f - Accuracy);

			for (int i = 0; i < count; i++) {
				float newAngle = angle - (angleDiff / 2) + (angleDiff / count * i);
				newAngle *= Mathf.Deg2Rad;

				Vector3 bulletDir = new Vector3(Mathf.Cos(newAngle), 0, Mathf.Sin(newAngle));
				bullets[i] = FireAtPos(position, bulletDir, speed, callback, ignoreCollision);
			}

			return bullets;
		}

		protected virtual Projectile FireAtPos (Vector3 position, Vector3 direction, float speed, Action<HitInfo> callback, params GameObject[] ignoreCollision) {
			Projectile b = Instantiate(GameManager.Resources.getPrefab("bullet"), position, Quaternion.Euler(Vector3.zero)).GetComponent<Projectile>();
			b.Initialize(direction, speed, callback, ignoreCollision);
			return b;
		}
	}
}