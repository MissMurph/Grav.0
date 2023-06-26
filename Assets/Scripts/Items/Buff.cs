using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff {

	protected float time;

	protected Entity parentEntity;

	protected BuffElementUI uiElement;

	public string name;

	protected List<IModifier> eModifiers = new List<IModifier>();

	public virtual void Tick () {
		time -= 1 * GameManager.tickRate;

		UpdateUIElement();

		if (time <= 0) Decay();
	}

	protected virtual void Decay () {
		foreach (IModifier m in eModifiers) {
			parentEntity.RemoveModifier(m);
		}
		parentEntity.RemoveBuff(this);
	}

	protected virtual void ApplyModifier (IModifier mod) {
		parentEntity.AddModifier(mod);
		eModifiers.Add(mod);
	}

	protected virtual void CreateUIElement () {
		uiElement = GameManager.UI_BuffController.AddBuffUIElement(name, Mathf.RoundToInt(time));
	}

	protected virtual void UpdateUIElement () {
		GameManager.UI_BuffController.UpdateTimer(uiElement, Mathf.RoundToInt(time));
	}
}

public class SuperLifeRegen : Buff {

	public SuperLifeRegen (Entity _entity, float _time) {
		parentEntity = _entity;

		name = "Super Life Regen!";
		time = _time;

		ApplyModifier(new HealthRegenAdder(5f));
		CreateUIElement();
	}
}