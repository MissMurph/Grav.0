using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

using Grav;
using Grav.Items;

namespace Grav.Entities {

	public class Entity : MonoBehaviour {

		public int MaxHealth { get { return BaseMaxHealth + modMaxHealth; } }
		public int BaseMaxHealth { get; private set; }
		protected int modMaxHealth;

		public int currentHealth;


		public float HealthRegen { get { return BaseHealthRegen + modHealthRegen; } }
		public float BaseHealthRegen { get; protected set; }
		protected float modHealthRegen;

		public float testHealthRegen;

		public float MoveSpeed { get { return BaseMoveSpeed + modMoveSpeed; } }
		public float BaseMoveSpeed { get; protected set; }
		protected float modMoveSpeed;

		protected List<Item> inventory = new List<Item>();

		protected Rigidbody rigidBody;

		//public Vector2 Direction { get { return _direction; } }
		//protected Vector2 _direction;

		public Vector2 Velocity { get { return _velocity; } }
		protected Vector2 _velocity;

		public float Angle { get { return _angle; } }
		protected float _angle;

		private List<IModifier> entityModifiiers = new List<IModifier>();

		private List<Buff> buffs = new List<Buff>();

		protected virtual void Awake () {
			BaseMaxHealth = 100;

			currentHealth = MaxHealth;
			rigidBody = GetComponent<Rigidbody>();
			BaseHealthRegen = 0.5f;
		}

		protected void Start () {

		}

		protected virtual void Update () {
			_velocity = rigidBody.velocity;
			testHealthRegen = HealthRegen;
		}

		public void Tick () {
			for (int i = 0; i < buffs.Count; i++) {
				buffs[i].Tick();
			}
		}

		public virtual void DealDamage (int damage) {
			currentHealth -= damage;
			if (currentHealth <= 0) Destroy(this.gameObject);
		}

		public virtual void AddForce (Vector2 force, float magnitude) {
			rigidBody.AddForce(force * magnitude);
		}

		public void AddModifier (IModifier mod) {
			entityModifiiers.Add(mod);
			mod.ApplyModifier(this);
		}

		public void RemoveModifier (IModifier mod) {
			entityModifiiers.Remove(mod);
			mod.RemoveModifier(this);
		}

		public virtual void AddBuff (Buff b) {
			buffs.Add(b);
		}

		public virtual void RemoveBuff (Buff b) {
			buffs.Remove(b);
		}

		public virtual void AddModMaxHealth (int value) {
			modMaxHealth += value;
		}

		public virtual void AddModHealthRegen (float value) {
			modHealthRegen += value;
		}

		public virtual void AddModMoveSpeed (float value) {
			modMoveSpeed += value;
		}
	}
}