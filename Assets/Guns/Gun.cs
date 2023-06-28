using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav.Entities;
using Grav.Items;
using UnityEngine.InputSystem;
using Grav.Players;
using Grav.Events;

namespace Grav.Guns {

	public abstract class Gun : Item {

		protected List<IGunPart> parts = new List<IGunPart>();

		public int Damage;

		//How many bullets per second can be fired from this gun
		public float FireRate;

		protected float fireDelay;
		protected float cooldownTimer;

		//This bool determines if the gun is ready to fire or not
		//This will check if:
		//The gun is being reloaded
		//The magazine has ammo in it
		//The firing cooldown has lapsed
		protected bool Chambered {
			get {
				return !isReloading && CurrentAmmo > 0 && cooldownTimer >= fireDelay;
			}
		}

		public float Recoil;

		public float Accuracy;

		public int MagSize;
		public int CurrentAmmo;

		public float ReloadTime;
		protected bool isReloading;

		protected virtual void Awake () {
			fireDelay = 1f / FireRate;
			cooldownTimer = 0f;
		}

		protected virtual void Update () {
			if (cooldownTimer < fireDelay) cooldownTimer += Time.deltaTime;
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

		protected virtual Projectile FireProjectile (Vector3 direction, float speed, Action<HitInfo> callback, params GameObject[] ignoreCollision) {
			Projectile b = Instantiate(GameManager.Resources.getPrefab("bullet"), transform.position, Quaternion.Euler(90, Player.Instance.transform.rotation.y, 0)).GetComponent<Projectile>();
			b.Initialize(direction, speed, callback, ignoreCollision);
			return b;
		}

		protected virtual void Reload () {
			if (isReloading) return;
			else StartCoroutine(ReloadGun());
		}

		protected virtual IEnumerator ReloadGun () {
			if (CurrentAmmo == MagSize) yield break;

			AmmoChange _event = EventBus.Post(new AmmoChange(this, MagSize - CurrentAmmo));

			if (_event.Canceled) yield break;

			isReloading = true;
			yield return new WaitForSeconds(ReloadTime);

			CurrentAmmo = _event.Change;
			_event.Phase = Phase.Post;
			isReloading = false;

			EventBus.Post(_event);
		}
	}
}