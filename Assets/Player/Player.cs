using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Grav;
using Grav.Entities;
using Grav.Items;
using Grav.Guns;
using UnityEngine.InputSystem;

namespace Grav.Players {

	public class Player : Entity {

		public static Player Instance { get; set; }

		public float brakeForce = 1f;
		private bool braking = false;

		//private BoxCollider2D collider;
		 
		private Camera view;

		public PassiveItem item;
		//public Del handler;

		public Gun[] guns;
		public Gun currentGun;

		private Vector2 direction;
		private Vector2 mousePos;

		//private List<IGunModifier> gunModifiers = new List<IGunModifier>();

		protected override void Awake () {
			base.Awake();
			if (Instance != null) Destroy(this.gameObject);
			else Instance = this;

			guns = new Gun[4];

			BaseMoveSpeed = 5f;

			//guns[0] = gameObject.AddComponent<FlameThrower>();
			//guns[1] = gameObject.AddComponent<RocketLauncher>();
			//guns[2] = gameObject.AddComponent<ShotGun>();
			//guns[3] = gameObject.AddComponent<DoubleBarrelShotgun>();

			//foreach (Gun g in guns) {
				//g.ItemParent = this;
			//}

			//currentGun = guns[0];
			//currentGun.UpdateUI();

			view = Camera.main;

			//_direction = new Vector2(0, 0);

			//item = gameObject.AddComponent<AmmoBox>();
			//item.Initialize(this);

			//AddGunModifier(new DamageMultiplier(0.10f));


			//handler += item.DebugMessage;
			//handler("suck my ass");
		}

		//public delegate void Del (string message);

		protected override void Update () {
			base.Update();

			float x = braking ? rigidBody.velocity.x * -1 * 0.9f : direction.x;
			float y = braking ? rigidBody.velocity.z * -1 * 0.9f : direction.y;

			rigidBody.AddForce(new Vector3(x * MoveSpeed, 0, y * MoveSpeed));

			Vector3 worldPos = view.ScreenToWorldPoint(mousePos);
			Vector2 mouseDir = new Vector3(worldPos.x, 0, worldPos.z) - transform.position;
			_angle = (Mathf.Atan2(mouseDir.y, mouseDir.x)) * Mathf.Rad2Deg;

			transform.eulerAngles = new Vector3(0, 0, _angle);
		}

		public void Move (InputAction.CallbackContext context) {
			direction = context.ReadValue<Vector2>();
		}

		public void Look (InputAction.CallbackContext context) {
			mousePos = context.ReadValue<Vector2>();
		}

		public void Fire (InputAction.CallbackContext context) {

		}

		public void Zoom (InputAction.CallbackContext context) {

		}

		public void Interact (InputAction.CallbackContext context) {

		}

		public void Reload (InputAction.CallbackContext context) {

		}

		public void Brake (InputAction.CallbackContext context) {
			if (context.started) braking = true;
			if (context.canceled) braking = false; 
		}

		public void Switch (InputAction.CallbackContext context) {
			Debug.Log(context.ReadValue<int>());
		}
	}
}