using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PotatoCore;

public class Player : Entity {

	public static Player Instance { get; set; }

	public float brakeForce = 1f;

	//private BoxCollider2D collider;

	private Camera gameCamera;

	public PassiveItem item;
	//public Del handler;

	public Gun[] guns;
	public Gun currentGun;

	public DelOnShoot onShootHandler;

	private List<IGunModifier> gunModifiers = new List<IGunModifier>();

	protected override void Awake () {
		base.Awake();
		if (Instance != null) Destroy(this.gameObject);
		else Instance = this;

		guns = new Gun[4];

		BaseMoveSpeed = 5f;

		guns[0] = gameObject.AddComponent<FlameThrower>();
		guns[1] = gameObject.AddComponent<RocketLauncher>();
		guns[2] = gameObject.AddComponent<ShotGun>();
		guns[3] = gameObject.AddComponent<DoubleBarrelShotgun>();

		foreach (Gun g in guns) {
			g.ItemParent = this;
		}

		currentGun = guns[0];
		currentGun.UpdateUI();

		gameCamera = Camera.main;

		_direction = new Vector2(0, 0);

		item = gameObject.AddComponent<AmmoBox>();
		item.Initialize(this);

		AddGunModifier(new DamageMultiplier(0.10f));
		

		//handler += item.DebugMessage;
		//handler("suck my ass");
	}

	//public delegate void Del (string message);

	protected override void Update () {
		base.Update();

		/*	Movement Inputs	*/
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		if (Input.GetAxisRaw("Jump") > 0) {
			x = (rigidBody.velocity.x * -1 * 0.9f);
			y = (rigidBody.velocity.y * -1 * 0.9f);

			
		}

		rigidBody.AddForce(new Vector2(x * MoveSpeed, y * MoveSpeed));

		/*	Keyboard Inputs	*/

		if (Input.GetKeyDown("r")) ReloadKeyDown();

		if (Input.GetKeyDown(KeyCode.Alpha1)) WeaponSwitch(0);
		if (Input.GetKeyDown(KeyCode.Alpha2)) WeaponSwitch(1);
		if (Input.GetKeyDown(KeyCode.Alpha3)) WeaponSwitch(2);
		if (Input.GetKeyDown(KeyCode.Alpha4)) WeaponSwitch(3);

		/*	Mouse Tracking	*/
		Vector2 mousePos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
		_direction.x = mousePos.x - transform.position.x; _direction.y = mousePos.y - transform.position.y;
		_angle = (Mathf.Atan2(_direction.y, _direction.x)) * Mathf.Rad2Deg;

		transform.eulerAngles = new Vector3(0, 0, _angle);

		/*	Mouse Inputs	*/
		if (Input.GetMouseButtonDown(0)) LeftMouseButtonDown();
		if (Input.GetMouseButton(0)) LeftMouseButton();
		if (Input.GetMouseButtonUp(0)) LeftMouseButtonUp();

		if (Input.GetMouseButtonDown(1)) RightMouseButtonDown();
		if (Input.GetMouseButton(1)) RightMouseButton();
		if (Input.GetMouseButtonUp(1)) RightMouseButtonUp();
	}

	public void LeftMouseButtonDown () {
		currentGun.LeftMouseButtonDown();
	}
	public void LeftMouseButton () {
		currentGun.LeftMouseButton();
	}
	public void LeftMouseButtonUp () {
		currentGun.LeftMouseButtonUp();
	}

	public void RightMouseButtonDown () {
		AddBuff(new SuperLifeRegen(this, 10f));
		currentGun.RightMouseButtonDown();
	}
	public void RightMouseButton () {
		currentGun.RightMouseButton();
	}
	public void RightMouseButtonUp () {
		currentGun.RightMouseButtonUp();
	}

	public void ReloadKeyDown () {
		StartCoroutine(currentGun.ReloadGun());
	}

	public void WeaponSwitch (int index) {
		currentGun = guns[index];
		guns[index].UpdateUI();
	}

	public void AddGunModifier (IGunModifier mod) {
		gunModifiers.Add(mod);
		foreach (Gun g in guns) {
			mod.ApplyModifier(g);
		}
	}

	public void RemoveGunModifier (IGunModifier mod) {
		gunModifiers.Remove(mod);
		foreach (Gun g in guns) {
			mod.RemoveModifier(g);
		}
	}


	public delegate void DelOnShoot (Gun gun);
	public void OnShoot (Gun gun) {
		onShootHandler(gun);
	}

	public delegate void DelOnHit (Entity target);
	public void OnHit (Entity target) {

	}
}