using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grav {

	public class ResourceManager : MonoBehaviour {

		public List<PrefabEntry> itemList = new List<PrefabEntry>();
		private Dictionary<string, GameObject> itemDictionary = new Dictionary<string, GameObject>();

		public List<PrefabEntry> uiElementList = new List<PrefabEntry>();
		private Dictionary<string, GameObject> uiElementDictionary = new Dictionary<string, GameObject>();

		private void Awake () {
			for (int i = 0; i < itemList.Count; i++) itemDictionary.Add(itemList[i].name, itemList[i].prefab);
			for (int i = 0; i < uiElementList.Count; i++) uiElementDictionary.Add(uiElementList[i].name, uiElementList[i].prefab);
		}

		public GameObject getPrefab (string name) {
			return itemDictionary[name];
		}

		public GameObject getUIElement (string name) {
			if (!uiElementDictionary.ContainsKey(name)) return null;
			return uiElementDictionary[name];
		}
	}

	[Serializable]
	public struct PrefabEntry {
		public string name;
		public GameObject prefab;
	}
}