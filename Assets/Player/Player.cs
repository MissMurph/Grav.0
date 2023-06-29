using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Grav;
using Grav.Entities;
using Grav.Items;
using Grav.Guns;
using UnityEngine.InputSystem;
using System;

namespace Grav.Players {

	public class Player : Entity {

		public static Player Instance { get; set; }

		public float brakeForce = 1f;
		private bool braking = false;
		 
		public static Camera ViewPort {
			get {
				return Instance.view;
			}
		}

		private Camera view;

		public PassiveItem item;

		public Gun[] guns;
		public Gun equipped;

		[SerializeField]
		private Vector2 moveDirection;

		public static Vector2 MousePosition {
			get {
				return Instance.mousePos;
			}
		}

		[SerializeField]
		private Vector2 mousePos;

		protected override void Awake () {
			base.Awake();
			if (Instance != null) Destroy(gameObject);
			else Instance = this;

			view = Camera.main;
		}

		protected override void Update () {
			base.Update();

			float x = braking ? rigidBody.velocity.x * -1 * 0.9f : moveDirection.x;
			float y = braking ? rigidBody.velocity.z * -1 * 0.9f : moveDirection.y;

			rigidBody.AddForce(new Vector3(x * MoveSpeed, 0, y * MoveSpeed));

			Vector3 worldPos = view.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 15));
			Vector3 mouseDir = new Vector3(worldPos.x - transform.position.x, 0, worldPos.z - transform.position.z).normalized;
			_direction = mouseDir;
			_angle = Mathf.Atan2(mouseDir.z, -mouseDir.x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler(0, _angle, 0);
		}

		public void Move (InputAction.CallbackContext context) {
			moveDirection = context.ReadValue<Vector2>();
		}

		public void Look (InputAction.CallbackContext context) {
			mousePos = context.ReadValue<Vector2>();
		}

		public void Fire (InputAction.CallbackContext context) {
			if (equipped != null) equipped.Trigger(context);
		}

		public void Zoom (InputAction.CallbackContext context) {
			//equipped.Zoom(context);
		}

		public void Interact (InputAction.CallbackContext context) {

		}

		public void Reload (InputAction.CallbackContext context) {
			equipped.Reload(context);
		}

		public void Brake (InputAction.CallbackContext context) {
			if (context.started) braking = true;
			if (context.canceled) braking = false; 
		}

		public void Switch (InputAction.CallbackContext context) {
			if (context.performed) {
				Vector2 value = context.ReadValue<Vector2>();
				//int index = context.ReadValue<int>();

				if (value.Equals(Vector2.up)) equipped = guns[0];
				else if (value.Equals(Vector2.left)) equipped = guns[1];
				else if (value.Equals(Vector2.down)) equipped = guns[2];
				else if (value.Equals(Vector2.right)) equipped = guns[3];
			}
		}
	}
}