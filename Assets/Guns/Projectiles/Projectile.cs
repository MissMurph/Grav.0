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

		public Vector3 Direction { get; protected set; }

		public float Speed { get; protected set; }

		protected bool initialized;

		protected Action<bool, Entity> callback;

		protected virtual void Awake () {
			RigidBody = GetComponent<Rigidbody>();
			initialized = false;
		}

		protected virtual void Update () {
			if (lifeTime <= 0f) Destroy(gameObject);

			lifeTime -= Time.deltaTime;
		}

		protected virtual void OnTriggerEnter2D (Collider2D collision) {
			if (collision.gameObject.CompareTag("Player")) return;
			if (!initialized) return;

			if (ignoreCollision.Contains(collision.gameObject)) return;

			if (collision.TryGetComponent(out Entity hit)) {
				callback(true, hit);
				Destroy(gameObject);
				return;
			}

			callback(false, null);
			Destroy(gameObject);
		}

		public virtual void Initialize (Vector3 _direction, float _speed, Action<bool, Entity> _callback, params GameObject[] _ignoreCollision) {
			if (initialized) return;

			Direction = _direction;
			Speed = _speed;
			callback = _callback;

			foreach (GameObject collider in _ignoreCollision) {
				ignoreCollision.Add(collider);
			}

			RigidBody.velocity = Direction * Speed;

			initialized = true;
		}

		protected virtual void IgnoreCollisions (Entity target) {
			Physics.IgnoreCollision(GetComponent<Collider>(), target.GetComponent<Collider>(), true);
		}
	}
}