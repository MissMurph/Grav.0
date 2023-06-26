using PotatoCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunPart {

	BaseStats.Rarities Rarity { get; set; }
}

public struct Action : IGunPart {
	public BaseStats.Rarities Rarity { get; set; }

	public Action (BaseStats.Rarities _rarity, Gun _parentGun) {
		Rarity = _rarity;

		int _dmgMod = (int)Enum.Parse(typeof(BaseStats.BaseDamageModifiers), Rarity.ToString());
		int _rateMod = (int)Enum.Parse(typeof(BaseStats.BaseFireRateModifiers), Rarity.ToString());

		float dmgMod = ((float)GameManager.RandomRange(-_dmgMod, _dmgMod)) / 100;
		float rateMod = ((float)GameManager.RandomRange(-_rateMod, _rateMod)) / 100;

		_parentGun.DamageModifier(dmgMod);
		_parentGun.FireRateModifier(rateMod);
	}
}

public struct Barrel : IGunPart {
	public BaseStats.Rarities Rarity { get; set; }

	public Barrel (BaseStats.Rarities _rarity, Gun _parentGun) {
		Rarity = _rarity;

		int _dmgMod = (int)Enum.Parse(typeof(BaseStats.BaseDamageModifiers), Rarity.ToString());
		int _accuMod = (int)Enum.Parse(typeof(BaseStats.BaseAccuModifiers), Rarity.ToString());

		float dmgMod = ((float)GameManager.RandomRange(-_dmgMod, _dmgMod)) / 100;
		float accuMod = ((float)GameManager.RandomRange(-_accuMod, _accuMod)) / 100;

		_parentGun.DamageModifier(dmgMod);
		_parentGun.AccuracyModifier(accuMod);
	}
}

public struct Stock : IGunPart {
	public BaseStats.Rarities Rarity { get; set; }

	public Stock (BaseStats.Rarities _rarity, Gun _parentGun) {
		Rarity = _rarity;

		int _recMod = (int)Enum.Parse(typeof(BaseStats.BaseRecoilModifiers), Rarity.ToString());
		int _accuMod = (int)Enum.Parse(typeof(BaseStats.BaseAccuModifiers), Rarity.ToString());

		float recMod = ((float)GameManager.RandomRange(-_recMod, _recMod)) / 100;
		float accuMod = ((float)GameManager.RandomRange(-_accuMod, _accuMod)) / 100;

		_parentGun.RecoilModifier(recMod);
		_parentGun.AccuracyModifier(accuMod);
	}
}

public struct Magazine : IGunPart {
	public BaseStats.Rarities Rarity { get; set; }

	public Magazine (BaseStats.Rarities _rarity, Gun _parentGun) {
		Rarity = _rarity;

		int _relMod = (int)Enum.Parse(typeof(BaseStats.BaseReloadSpeedModifiers), Rarity.ToString());
		int _magMod = (int)Enum.Parse(typeof(BaseStats.BaseMagSizeModifiers), Rarity.ToString());

		float relMod = ((float)GameManager.RandomRange(-_relMod, _relMod)) / 100;
		float magMod = ((float)GameManager.RandomRange(-_magMod, _magMod)) / 100;

		_parentGun.ReloadTimeModifier(relMod);
		_parentGun.MagSizeModifier(magMod);
	}
}

public struct Trigger : IGunPart {
	public BaseStats.Rarities Rarity { get; set; }

	public Trigger (BaseStats.Rarities _rarity, Gun _parentGun) {
		Rarity = _rarity;

		int _recMod = (int)Enum.Parse(typeof(BaseStats.BaseRecoilModifiers), Rarity.ToString());
		int _rateMod = (int)Enum.Parse(typeof(BaseStats.BaseFireRateModifiers), Rarity.ToString());

		float recMod = ((float)GameManager.RandomRange(-_recMod, _recMod)) / 100;
		float rateMod = ((float)GameManager.RandomRange(-_rateMod, _rateMod)) / 100;

		_parentGun.RecoilModifier(recMod);
		_parentGun.FireRateModifier(rateMod);
	}
}