using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

using Grav;
using Grav.Entities;
using Grav.Items;
using UnityEngine.InputSystem;
using Grav.Players;

namespace Grav.Guns {

	public abstract class Gun : Item {

		protected List<IGunPart> parts = new List<IGunPart>();

		public int Damage;

		public float FireRate;

		protected float fireDelay;

		public float Recoil;

		public float Accuracy;

		public int MagSize;

		public int CurrentAmmo;

		public float ReloadTime;
		protected bool isReloading;

		protected virtual void Update () {
			//if (cooldownTick > 0) cooldownTick -= Time.deltaTime;
		}

		public abstract void Trigger (InputAction.CallbackContext context);
		public abstract void Zoom (InputAction.CallbackContext context);
		public abstract void Reload (InputAction.CallbackContext context);

		protected virtual void FireRay (Vector3 direction, Action<bool, Entity> callback) {
			Debug.DrawRay(transform.position, direction, Color.yellow, 5f);

			if (Physics.Raycast(transform.position, direction, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent(out Entity e)) {
				callback(true, e);
			}
			else callback(false, null);
		}

		protected virtual Projectile FireProjectile (Vector3 direction, float speed, Action<bool, Entity> callback, params GameObject[] ignoreCollision) {
			Projectile b = Instantiate(GameManager.Resources.getPrefab("bullet"), transform.position, Quaternion.Euler(0, Player.Instance.transform.rotation.y, 0)).GetComponent<Projectile>();
			b.Initialize(direction, speed, callback, ignoreCollision);
			return b;
		}

		protected virtual IEnumerator ReloadGun () {
			if (CurrentAmmo == MagSize) yield break;
			yield return new WaitForSeconds(ReloadTime);
		}
	}
}