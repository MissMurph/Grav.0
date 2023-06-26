using Grav.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Guns {

	public class Explosion : Projectile {

		protected BoxCollider eCollider;

		public float delayTime;

		public int damage;

		protected bool exploding;

		protected RocketLauncher parentGun;

		protected float lifeTime = 1f;      //This is a temporary system until we add animation based explosion lifetimes

		protected void Update () {
			if (delayTime <= 0) Explode();

			if (lifeTime <= 0) Destroy(this.gameObject);
			lifeTime -= Time.deltaTime;
		}

		protected void Explode () {
			eCollider.enabled = true;
		}

		protected void OnTriggerEnter (Collider collision) {
			if (collision.TryGetComponent<Entity>(out Entity e)) {
				e.DealDamage(damage);
				OnHit(e);
			}
		}

		protected virtual void OnHit (Entity e) {
			parentGun.ProjectileHit(e, this);
		}

		public void Initialize (int _damage, RocketLauncher _parentGun, float _delayTime = 0f) {
			delayTime = _delayTime;
			damage = _damage;
			parentGun = _parentGun;

			eCollider = GetComponent<BoxCollider>();
			eCollider.enabled = false;
		}
	}
}