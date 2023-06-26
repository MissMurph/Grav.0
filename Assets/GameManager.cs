using Grav.Entities;
using Grav.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grav {

	public class GameManager : MonoBehaviour {
		public static GameManager Instance { get; private set; }
		public static ResourceManager Resources { get; private set; }
		public static System.Random RandomGenerator { get; private set; }

		public static ActiveGunUI UI_ActiveGun { get; private set; }
		public static BuffControllerUI UI_BuffController { get; private set; }

		public GameObject[] objectsToInitialize;

		public static List<Entity> activeEntities = new List<Entity>();

		public static float tickRate = 1f;
		private float timeTilNextTick;

		public static LayerMask GetLayerMask (int index) {
			if (index < 0 || index > _layerMasks.Length) Debug.LogError("LayerMask Index out of Range");

			return _layerMasks[index];
		}
		private static LayerMask[] _layerMasks;

		public static int RandomRange (int min, int max) {
			return RandomGenerator.Next(min, max + 1);
		}

		private void Awake () {
			if (Instance != null && Instance != this) Destroy(this.gameObject);
			else Instance = this;

			timeTilNextTick = tickRate;

			Resources = GetComponent<ResourceManager>();
			RandomGenerator = new System.Random();

			_layerMasks = new LayerMask[1];
			_layerMasks[0] = LayerMask.GetMask("Collideable");

			UI_ActiveGun = GameObject.Find("AmmoCounter").GetComponent<ActiveGunUI>();
			UI_BuffController = GameObject.Find("BuffSection").GetComponent<BuffControllerUI>();

			InitializeGame();
		}

		private void Update () {
			UI_ActiveGun.ReloadingText.transform.position = Input.mousePosition;

			if (timeTilNextTick <= 0) {
				foreach (Entity e in activeEntities) {
					e.Tick();
				}

				timeTilNextTick = tickRate;
			}

			timeTilNextTick -= Time.deltaTime;
		}

		private void InitializeGame () {
			foreach (GameObject g in objectsToInitialize) {
				g.SetActive(true);

				if (g.TryGetComponent<Entity>(out Entity e)) activeEntities.Add(e);
			}


		}

		public static void UpdateText (Text textToUpdate, string text) {
			textToUpdate.text = text;
		}

		public static void ActivateReloading (bool value) {
			UI_ActiveGun.ReloadingText.SetActive(value);
		}
	}
}