using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBullet : Projectile {

	protected float deathTime = 3f;

	protected int damage;

	protected Gun parentGun;

	protected List<GameObject> ignoreCollision = new List<GameObject>();

	public Rigidbody2D RigidBody { get; protected set; }

	public Vector2 Direction { get; protected set; }

	public float Speed { get; protected set; }

	protected bool initialized;

	protected virtual void Awake () {
		RigidBody = GetComponent<Rigidbody2D>();
		initialized = false;
	}

	protected virtual void Update () {
		if (deathTime <= 0f) Destroy(this.gameObject);

		deathTime -= Time.deltaTime;
	}

	protected virtual void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) return;
		if (!initialized) return;

		if (ignoreCollision.Contains(collision.gameObject)) return;

		if (collision.TryGetComponent<Entity>(out Entity e)) {
			//e.DealDamage(damage);
			parentGun.ProjectileHit(e, this);
		}

		Destroy(this.gameObject);
	}

	public void Initialize (int _damage, Gun _parentGun, Vector2 _direction, float _speed, params GameObject[] _ignoreCollision) {
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

	public virtual void IgnoreCollisions (Entity e) {
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), e.GetComponent<Collider2D>(), true);
	}
}

public class Projectile : MonoBehaviour {

}