using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Grav;

namespace Grav.Guns {

	public interface IAccessory : IGunPart {
		//IGunPart ReqPart { get; set; }
	}

	public struct IronSight : IAccessory {

		public BaseStats.Rarities Rarity { get; set; }
		//public IGunPart ReqPart { get; set; }

		public IronSight (BaseStats.Rarities _rarity, Gun _parentGun) {
			Rarity = _rarity;

			//Type type = typeof Action();

			int _dmgMod = (int)Enum.Parse(typeof(BaseStats.BaseDamageModifiers), Rarity.ToString());
			int _rateMod = (int)Enum.Parse(typeof(BaseStats.BaseFireRateModifiers), Rarity.ToString());

			float dmgMod = ((float)GameManager.RandomGenerator.Next(-_dmgMod, _dmgMod + 1)) / 100;
			float rateMod = ((float)GameManager.RandomGenerator.Next(-_rateMod, _rateMod + 1)) / 100;

			_parentGun.DamageModifier(dmgMod);
			_parentGun.FireRateModifier(rateMod);
		}
	}
}