using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PotatoCore;

public class ShotGun : Gun {
	protected int shotCount;

	//public override int Damage { get { return (baseDamage + modDamage) * shotCount; } }

	protected virtual void Awake () {
		ItemName = "Shotgun";

		baseDamage = 5;
		baseFireRate = 5f;
		baseRecoil = 1f;
		baseAccuracy = 0.95f;
		baseMagSize = 5;
		baseReloadTime = 4f;

		shotCount = 7;

		parts.Add(new Action(0, this));
		parts.Add(new Barrel(0, this));
		parts.Add(new Stock(0, this));
		parts.Add(new Magazine(0, this));
		parts.Add(new Trigger(0, this));

		fireDelay = 1f / FireRate;

		InitializeAmmo();
	}

	public override void LeftMouseButtonDown () {
		FireGun(ItemParent.Direction);
	}

	protected override void FireGun (Vector2 direction) {
		if (_currentAmmo <= 0) { StartCoroutine(ReloadGun()); return; }
		if (!isReloading && cooldownTick > 0) return;

		ItemParent.AddForce(-direction, Recoil * Damage * 2);

		float angle = Mathf.Atan2(direction.y, direction.x);
		angle *= Mathf.Rad2Deg;
		float angleDiff = 360 * (1f - Accuracy);

		for (int i = 0; i < shotCount; i++) {
			float newAngle = (angle - (angleDiff / 2)) + (angleDiff * ((float)GameManager.RandomGenerator.Next(0, 100) / 100f));
			newAngle *= Mathf.Deg2Rad;

			direction = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));

			FireRay(direction, Damage);
		}

		_currentAmmo -= AmmoConsumption;
		GameManager.UpdateText(GameManager.UI_ActiveGun.CurrentAmmoCounter, CurrentAmmo.ToString());
		cooldownTick = fireDelay;
	}
}

public class DoubleBarrelShotgun : ShotGun {

	protected override void Awake () {
		base.Awake();

		ItemName = "Double Barrel Shotgun";

		baseMagSize = 2;
		modMagSize = 0;

		InitializeAmmo();
	}

	public override void LeftMouseButton () {
		base.LeftMouseButton();
	}

	public override void LeftMouseButtonUp () {
		base.LeftMouseButtonUp();
		FireGun(ItemParent.Direction);
	}
}