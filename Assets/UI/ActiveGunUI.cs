using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grav.UI {

	public class ActiveGunUI : MonoBehaviour {

		public Text CurrentAmmoCounter { get; private set; }
		public Text MagSizeCounter { get; private set; }
		public Text ActiveGunName { get; private set; }
		public GameObject ReloadingText { get; private set; }

		private void Awake () {
			CurrentAmmoCounter = GameObject.Find("CurrentAmmo").GetComponent<Text>();
			MagSizeCounter = GameObject.Find("MagSize").GetComponent<Text>();
			ActiveGunName = GameObject.Find("GunName").GetComponent<Text>();
			ReloadingText = GameObject.Find("ReloadingText");

			ReloadingText.SetActive(false);
		}
	}
}