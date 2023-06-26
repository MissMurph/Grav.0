using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;

namespace Grav.Guns {

	public class Pistol : Gun {

		protected virtual void Awake () {
			ItemName = "Pistol";

			baseDamage = 15;
			baseFireRate = 2f;
			baseRecoil = 1f;
			baseAccuracy = 0.98f;
			baseMagSize = 7;
			baseReloadTime = 2f;

			parts.Add(new Action(0, this));
			parts.Add(new Barrel(0, this));
			parts.Add(new Magazine(0, this));
			parts.Add(new Trigger(0, this));

			fireDelay = 1f / FireRate;

			InitializeAmmo();
		}
	}
}