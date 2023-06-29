using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

using Grav;
using Grav.Items;

namespace Grav.Entities {

	public class Entity : MonoBehaviour {

		public int MaxHealth;

		public int currentHealth;

		public float HealthRegen;

		public float MoveSpeed;

		protected List<Item> inventory = new List<Item>();

		protected Rigidbody rigidBody;

		public Vector3 Direction { get { return _direction; } }
		[SerializeField]
		protected Vector3 _direction;

		public Vector2 Velocity { get { return rigidBody.velocity; } }

		public float Angle { get { return _angle; } }
		protected float _angle;

		protected virtual void Awake () {
			MaxHealth = 100;

			currentHealth = MaxHealth;
			rigidBody = GetComponent<Rigidbody>();
		}

		protected void Start () {

		}

		protected virtual void Update () {

		}

		public void Tick () {

		}

		public virtual void Attack (int damage) {
			currentHealth -= damage;
			if (currentHealth <= 0) Destroy(gameObject);
		}
	}
}