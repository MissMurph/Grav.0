using Grav.Guns;
using Grav.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Items {

	public class Item : MonoBehaviour {

		public Player ItemParent { get; set; }

		public void SetParent (Player p) {
			ItemParent = p;
		}

		public virtual string ItemName { get; protected set; }
	}

	public class PassiveItem : Item {

		public virtual void DebugMessage (string message) {
			Debug.Log(message);
		}

		public virtual void Initialize (Player parent) {
			ItemParent = parent;
		}
	}

	public class AmmoBox : PassiveItem {

		public override void Initialize (Player parent) {
			base.Initialize(parent);

			//parent.onShootHandler += OnShoot;
		}

		private void OnShoot (Gun gun) {
			if (GameManager.RandomGenerator.Next(0, 100) <= 20) gun._currentAmmo += gun.AmmoConsumption;
		}
	}

	public class ExtraBarrel : PassiveItem {
		public override void Initialize (Player parent) {
			base.Initialize(parent);

			//parent.onShootHandler += OnShoot;
		}

		private void OnShoot (Gun gun) {
			//gun.FireRay(gun.Damage);
		}
	}
}