using Grav;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav.Guns {

	public interface IGunPart {

		BaseStats.Rarities Rarity { get; set; }
	}
}