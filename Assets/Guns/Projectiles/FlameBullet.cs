using Grav;
using Grav.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Guns {

	public class FlameBullet : Projectile {
		public int SpreadChain { get; protected set; }

		protected float deathTime = 3f;

		protected int damage;

		protected FlameThrower parentGun;

		protected List<GameObject> ignoreCollision = new List<GameObject>();

		public Rigidbody RigidBody { get; protected set; }

		public Vector2 Direction { get; protected set; }

		public float Speed { get; protected set; }

		protected bool initialized;

		protected virtual void Awake () {
			RigidBody = GetComponent<Rigidbody>();
			initialized = false;
		}

		protected virtual void Update () {
			if (deathTime <= 0f) Destroy(this.gameObject);

			deathTime -= Time.deltaTime;
		}

		public virtual void Initialize (int _damage, FlameThrower _parentGun, Vector2 _direction, float _speed, int _spreadChain, params GameObject[] _ignoreCollision) {
			SpreadChain = _spreadChain;

			damage = _damage;
			Direction = _direction;
			Speed = _speed;

			parentGun = _parentGun;

			foreach (GameObject g in _ignoreCollision) {
				ignoreCollision.Add(g);
			}

			RigidBody.velocity = Direction * Speed;

			initialized = true;
		}

		protected virtual void OnTriggerEnter (Collider collision) {
			if (collision.gameObject.CompareTag("Player")) return;
			if (!initialized) return;

			if (ignoreCollision.Contains(collision.gameObject)) return;

			if (collision.TryGetComponent<Entity>(out Entity e)) {
				//e.DealDamage(damage);
				parentGun.ProjectileHit(e, this);
			}

			Destroy(this.gameObject);
		}

		public virtual void IgnoreCollisions (Entity e) {
			Physics.IgnoreCollision(GetComponent<Collider>(), e.GetComponent<Collider>(), true);
		}
	}
}