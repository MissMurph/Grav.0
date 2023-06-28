using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grav.Events;
using Grav.Guns;

namespace Grav.Events {

	public class AmmoChange : AbstractEvent {

		public Gun Target { get; private set; }
		public int Change { get; set; }

		public AmmoChange (Gun _target, int _change) : base("ammo_change") {
			Target = _target;
			Change = _change;
		}
	}
}