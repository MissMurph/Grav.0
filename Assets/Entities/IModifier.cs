using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Grav;

namespace Grav.Entities {

	public interface IModifier {

		void ApplyModifier (Entity e);
		void RemoveModifier (Entity e);
	}
}

/*
public struct MoveSpeedMultiplier : IModifier {
	private float mod;

	public MoveSpeedMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Entity e) {
		e.AddModMoveSpeed(e.BaseMoveSpeed * mod);
	}

	public void RemoveModifier (Entity e) {
		e.AddModMoveSpeed(e.BaseMoveSpeed * -mod);
	}
}

public struct MaxHealthMultiplier : IModifier {
	private float mod;

	public MaxHealthMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Entity e) {
		e.AddModMaxHealth(Mathf.RoundToInt(e.BaseMaxHealth * mod));
	}

	public void RemoveModifier (Entity e) {
		e.AddModMaxHealth(Mathf.RoundToInt(e.BaseMaxHealth * -mod));
	}
}
public struct HealthRegenMultiplier : IModifier {
	private float mod;

	public HealthRegenMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Entity e) {
		e.AddModHealthRegen(e.BaseHealthRegen * mod);
	}

	public void RemoveModifier (Entity e) {
		e.AddModHealthRegen(e.BaseHealthRegen * -mod);
	}
}

public struct HealthRegenAdder : IModifier {
	private float mod;

	public HealthRegenAdder (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Entity e) {
		e.AddModHealthRegen(mod);
	}

	public void RemoveModifier (Entity e) {
		e.AddModHealthRegen(-mod);
	}
}

public interface IGunModifier {
	void ApplyModifier (Gun gun);

	void RemoveModifier (Gun gun);
}

public struct DamageMultiplier : IGunModifier {

	private float mod;

	public DamageMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Gun gun) {
		gun.eModDamage += Mathf.RoundToInt(gun.baseDamage * mod);
	}

	public void RemoveModifier (Gun gun) {
		gun.eModDamage -= Mathf.RoundToInt(gun.baseDamage * mod);
	}
}

public struct FireRateMultiplier : IGunModifier {

	private float mod;

	public FireRateMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Gun gun) {
		gun.eModFireRate += Mathf.RoundToInt(gun.baseFireRate * mod);
	}

	public void RemoveModifier (Gun gun) {
		gun.eModFireRate -= Mathf.RoundToInt(gun.baseFireRate * mod);
	}
}
public struct RecoilMultiplier : IGunModifier {

	private float mod;

	public RecoilMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Gun gun) {
		gun.eModRecoil += Mathf.RoundToInt(gun.baseRecoil * mod);
	}

	public void RemoveModifier (Gun gun) {
		gun.eModRecoil -= Mathf.RoundToInt(gun.baseRecoil * mod);
	}
}
public struct AccuracyMultiplier : IGunModifier {

	private float mod;

	public AccuracyMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Gun gun) {
		gun.eModAccuracy += Mathf.RoundToInt(gun.baseAccuracy * mod);
	}

	public void RemoveModifier (Gun gun) {
		gun.eModAccuracy -= Mathf.RoundToInt(gun.baseAccuracy * mod);
	}
}
public struct MagSizeMultiplier : IGunModifier {

	private float mod;

	public MagSizeMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Gun gun) {
		gun.eModMagSize += Mathf.RoundToInt(gun.baseMagSize * mod);
	}

	public void RemoveModifier (Gun gun) {
		gun.eModMagSize -= Mathf.RoundToInt(gun.baseMagSize * mod);
	}
}
public struct ReloadSpeedMultiplier : IGunModifier {

	private float mod;

	public ReloadSpeedMultiplier (float _mod) {
		mod = _mod;
	}

	public void ApplyModifier (Gun gun) {
		gun.eModReloadTime += Mathf.RoundToInt(gun.baseReloadTime * mod);
	}

	public void RemoveModifier (Gun gun) {
		gun.eModReloadTime -= Mathf.RoundToInt(gun.baseReloadTime * mod);
	}
}
*/