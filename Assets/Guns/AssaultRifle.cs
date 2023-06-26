using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav;

namespace Grav.Guns {

	public class AssaultRifle : Gun {

		protected virtual void Awake () {
			ItemName = "Assault Rifle";

			baseDamage = 10;
			baseFireRate = 7f;
			baseRecoil = 1f;
			baseAccuracy = 0.98f;
			baseMagSize = 30;
			baseReloadTime = 3.5f;

			parts.Add(new Action(BaseStats.Rarities.Legendary, this));
			parts.Add(new Barrel(BaseStats.Rarities.Legendary, this));
			parts.Add(new Stock(BaseStats.Rarities.Legendary, this));
			parts.Add(new Magazine(BaseStats.Rarities.Legendary, this));
			parts.Add(new Trigger(BaseStats.Rarities.Legendary, this));

			fireDelay = 1f / FireRate;

			InitializeAmmo();
		}

		public override void LeftMouseButton () {
			FireGun(ItemParent.Direction);
		}
	}
}