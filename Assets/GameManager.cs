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

		public GameObject[] objectsToInitialize;

		public static List<Entity> activeEntities = new List<Entity>();

		public static float tickRate = 1f;
		private float timeTilNextTick;

		private void Awake () {
			if (Instance != null && Instance != this) Destroy(this.gameObject);
			else Instance = this;

			timeTilNextTick = tickRate;

			Resources = GetComponent<ResourceManager>();

			InitializeGame();
		}

		private void Update () {
		}

		private void InitializeGame () {

		}
	}
}