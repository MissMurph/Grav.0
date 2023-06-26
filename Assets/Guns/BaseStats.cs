using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Guns {

	public static class BaseStats {

		public enum Rarities {
			Common = 0,
			UnCommon = 1,
			Rare = 2,
			Legendary = 4,
			Exotic = 5,
			Stange = 6
		}

		/*	Percantage Modifier Possible	*/
		public enum BaseDamageModifiers {
			Common = 10,
			UnCommon = 15,
			Rare = 25,
			Legendary = 40
		}
		public enum BaseFireRateModifiers {
			Common = 10,
			UnCommon = 15,
			Rare = 25,
			Legendary = 40
		}
		public enum BaseAccuModifiers {
			Common = 5,
			UnCommon = 7,
			Rare = 10,
			Legendary = 15
		}
		public enum BaseRecoilModifiers {
			Common = 10,
			UnCommon = 15,
			Rare = 20,
			Legendary = 25
		}
		public enum BaseMagSizeModifiers {
			Common = 5,
			UnCommon = 10,
			Rare = 15,
			Legendary = 20
		}
		public enum BaseReloadSpeedModifiers {
			Common = 5,
			UnCommon = 10,
			Rare = 20,
			Legendary = 35
		}
	}
}