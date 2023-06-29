using Grav.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Guns {

	public class Projectile : MonoBehaviour {

		protected float lifeTime = 3f;

		protected Gun parentGun;

		protected List<GameObject> ignoreCollision = new List<GameObject>();

		public Rigidbody RigidBody { get; protected set; }

		public Vector3 Direction {
			get {
				return Direction;
			}
			set {
				Direction = value;
				transform.rotation = Quaternion.Euler(90, Mathf.Atan2(Direction.z, -Direction.x) * Mathf.Rad2Deg - 90, 0);
				RigidBody.velocity = Direction * Speed;
			}
		}

		public float Speed { get; protected set; }

		protected bool initialized;

		protected Action<HitInfo> callback;

		protected Collider localCollider;

		protected virtual void Awake () {
			RigidBody = GetComponent<Rigidbody>();
			localCollider = GetComponent<Collider>();
			initialized = false;
		}

		protected virtual void Update () {
			if (lifeTime <= 0f) Destroy(gameObject);

			lifeTime -= Time.deltaTime;
		}

		protected virtual void OnTriggerEnter (Collider collision) {
			if (!initialized) return;

			//if (ignoreCollision.Contains(collision.gameObject)) return;

			if (collision.TryGetComponent(out Entity hit)) {
				callback(new HitInfo { Target = hit, Bullet = this, Result = true});
				return;
			}

			callback(new HitInfo { Target = null, Bullet = this, Result = false });
		}

		public virtual void Initialize (Vector3 _direction, float _speed, Action<HitInfo> _callback, params GameObject[] _ignoreCollision) {
			if (initialized) return;

			Direction = _direction;
			//transform.rotation = Quaternion.Euler(90, Mathf.Atan2(Direction.z, -Direction.x) * Mathf.Rad2Deg - 90, 0);
			Speed = _speed;
			callback = _callback;
			localCollider.enabled = true;

			foreach (GameObject collider in _ignoreCollision) {
				ignoreCollision.Add(collider);
				IgnoreCollisions(collider);
			}

			initialized = true;
		}

		protected virtual void IgnoreCollisions (GameObject target) {
			Physics.IgnoreCollision(GetComponent<Collider>(), target.GetComponent<Collider>(), true);
		}
	}
}