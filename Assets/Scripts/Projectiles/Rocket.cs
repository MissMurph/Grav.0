using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile {
	protected float deathTime = 3f;

	protected int damage;

	protected RocketLauncher parentGun;

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

	protected virtual void OnTriggerEnter (Collider collision) {
		if (collision.gameObject.CompareTag("Player")) return;
		if (!initialized) return;

		if (ignoreCollision.Contains(collision.gameObject)) return;

		CreateExplosion(parentGun, damage, 0f);

		Destroy(this.gameObject);
	}

	protected virtual void CreateExplosion (RocketLauncher parentGun, int damage, float delayTime) {
		Explosion e = Instantiate(GameManager.Resources.getPrefab("explosion"), transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Explosion>();
		e.Initialize(damage, parentGun, 0f);
	}

	public void Initialize (int _damage, RocketLauncher _parentGun, Vector2 _direction, float _speed, params GameObject[] _ignoreCollision) {
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