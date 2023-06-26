using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

using PotatoCore;

public class Gun : Item {

	protected List<IGunPart> parts = new List<IGunPart>();

	public virtual int Damage { get { return baseDamage + modDamage + eModDamage; } }
	public int baseDamage;
	protected int modDamage;
	public int eModDamage;

	public virtual float FireRate { get { return baseFireRate + modFireRate + eModFireRate; } }
	public float baseFireRate;      //How many times it fires per second
	protected float modFireRate;
	public float eModFireRate;

	protected float cooldownTick;
	protected float fireDelay;

	public virtual float Recoil { get { return baseRecoil + modRecoil + eModRecoil; } }
	public float baseRecoil;
	protected float modRecoil;
	public float eModRecoil;

	public virtual float Accuracy { get { return Mathf.Clamp(baseAccuracy + modAccuracy + eModAccuracy, 0.98f, 1f); } }
	public float baseAccuracy;
	protected float modAccuracy;
	public float eModAccuracy;

	public virtual int MagSize { get { return baseMagSize + modMagSize + eModMagSize; } }
	public int baseMagSize;
	protected int modMagSize;
	public int eModMagSize;

	public virtual int CurrentAmmo { get { return _currentAmmo; } } 
	public int _currentAmmo;

	public int AmmoConsumption { get { return _ammoConsumption; } set { if (value <= 0) _ammoConsumption = 1; } }
	protected int _ammoConsumption = 1;

	public virtual float ReloadTime { get { return baseReloadTime + modReloadTime + eModReloadTime; } }
	public float baseReloadTime;
	protected float modReloadTime;
	public float eModReloadTime;
	protected bool isReloading;

	protected virtual void InitializeAmmo () {
		_currentAmmo = MagSize;
	}

	protected virtual void Update () {
		if (cooldownTick > 0) cooldownTick -= Time.deltaTime;
	}

	protected virtual void FireGun (Vector2 direction) {
		if (_currentAmmo <= 0) { StartCoroutine(ReloadGun()); return; }
		if (isReloading || cooldownTick > 0) return;

		ItemParent.AddForce(-direction, Recoil * Damage);

		float angle = Mathf.Atan2(direction.y, direction.x);
		angle *= Mathf.Rad2Deg;
		float angleDiff = 360 * (1f - Accuracy);
		angle = (angle - (angleDiff / 2)) + (angleDiff * ((float)GameManager.RandomGenerator.Next(0, 100) / 100f));
		angle *= Mathf.Deg2Rad;

		direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

		FireRay(direction, Damage);

		_currentAmmo -= AmmoConsumption;

		OnShoot(this);
		GameManager.UpdateText(GameManager.UI_ActiveGun.CurrentAmmoCounter, CurrentAmmo.ToString());
		cooldownTick = fireDelay;
	}

	public virtual void FireRay (Vector2 direction, int rayDamage) {
		RaycastHit2D hit = Physics2D.Raycast(ItemParent.transform.position, direction, 100f, GameManager.GetLayerMask(0));
		Debug.DrawRay(ItemParent.transform.position, direction, Color.yellow, 5f);

		if (hit.collider == null) return;

		if (hit.collider.gameObject.TryGetComponent<Entity>(out Entity e)) {
			RayHit(e);
			e.DealDamage(rayDamage);
		}
	}

	public void UpdateUI () {
		GameManager.UpdateText(GameManager.UI_ActiveGun.ActiveGunName, ItemName);
		GameManager.UpdateText(GameManager.UI_ActiveGun.CurrentAmmoCounter, CurrentAmmo.ToString());
		GameManager.UpdateText(GameManager.UI_ActiveGun.MagSizeCounter, MagSize.ToString());
	}

	public virtual GenericBullet FireProjectile (Vector2 direction, float angle, int damage, float speed, Vector2 position, params GameObject[] ignoreCollision) {
		GenericBullet b = Instantiate(GameManager.Resources.getPrefab("bullet"), position, Quaternion.Euler(0, 0, angle)).GetComponent<GenericBullet>();
		b.Initialize(damage, this, direction, speed, ignoreCollision);
		return b;
	}

		/*	These have to be implemented and overriden by gun classes	*/
	public virtual void ProjectileHit (Entity e, Projectile p) {
		OnHit(e);
	}

	public virtual void RayHit (Entity e) {
		OnHit(e);
	}

	public virtual IEnumerator ReloadGun () {
		if (CurrentAmmo == MagSize) yield break;
		
		isReloading = true;
		GameManager.ActivateReloading(true);

		yield return new WaitForSeconds(ReloadTime);

		_currentAmmo = MagSize;
		GameManager.UpdateText(GameManager.UI_ActiveGun.CurrentAmmoCounter, CurrentAmmo.ToString());
		isReloading = false;
		GameManager.ActivateReloading(false);
	}

	protected virtual void GenerateGun () {

	}

		/*	Input interpreters, Player will call these functions	*/
	public virtual void LeftMouseButtonDown () { }
	public virtual void LeftMouseButton () { }
	public virtual void LeftMouseButtonUp () { }
	public virtual void RightMouseButtonDown () { }
	public virtual void RightMouseButton () { }
	public virtual void RightMouseButtonUp () { }

	protected virtual void OnShoot (Gun gun) {
		ItemParent.OnShoot(gun);
	}
	protected virtual void OnHit (Entity target) {
		ItemParent.OnHit(target);
	}
	//protected virtual void OnMiss () { }
	//protected virtual void OnReloadFull () { }
	//protected virtual void OnReloadPartial () { }

	public virtual void DamageModifier (float multiplier) {
		modDamage += Mathf.RoundToInt(baseDamage * multiplier);
	}

	public virtual void FireRateModifier (float multiplier) {
		modFireRate += baseFireRate * multiplier;
	}

	public virtual void RecoilModifier (float multiplier) {
		modRecoil += baseRecoil * multiplier;
	}

	public virtual void AccuracyModifier (float multiplier) {
		modAccuracy += baseAccuracy * multiplier;
	}

	public virtual void MagSizeModifier (float multiplier) {
		modMagSize += Mathf.RoundToInt(baseMagSize * multiplier);
	}

	public virtual void ReloadTimeModifier (float multiplier) {
		modReloadTime += baseReloadTime * multiplier;
	}
}