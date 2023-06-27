using Grav.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Guns {

	public class Explosion : MonoBehaviour {

		protected BoxCollider eCollider;

		public float delayTime;

		protected float lifeTime = 1f;      //This is a temporary system until we add animation based explosion lifetimes
		protected bool initialized;

		protected List<GameObject> ignoreCollision = new List<GameObject>();

		protected Action<bool, Entity> callback;

		protected void Update () {
			if (delayTime <= 0) Explode();

			if (lifeTime <= 0) Destroy(gameObject);
			lifeTime -= Time.deltaTime;
		}

		protected void Explode () {
			eCollider.enabled = true;
		}

		protected void OnTriggerEnter (Collider collision) {
			if (collision.gameObject.CompareTag("Player")) return;
			if (!initialized) return;

			if (ignoreCollision.Contains(collision.gameObject)) return;

			if (collision.TryGetComponent(out Entity hit)) {
				callback(true, hit);
			}
		}

		public void Initialize (float _delay, float _lifetime, Action<bool, Entity> _callback, params GameObject[] _ignoreCollision) {
			if (initialized) return;
			
			delayTime = _delay;
			lifeTime = _lifetime;

			eCollider = GetComponent<BoxCollider>();
			eCollider.enabled = false;

			foreach (GameObject collider in _ignoreCollision) {
				ignoreCollision.Add(collider);
			}

			initialized = true;
		}

		protected virtual void IgnoreCollisions (Entity target) {
			Physics.IgnoreCollision(GetComponent<Collider>(), target.GetComponent<Collider>(), true);
		}
	}
}