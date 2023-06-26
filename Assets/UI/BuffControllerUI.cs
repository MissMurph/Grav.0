using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grav.UI {

	public class BuffControllerUI : MonoBehaviour {
		private int x;

		private void Start () {
			x = 0;
		}

		public BuffElementUI AddBuffUIElement (string name, int time) {
			BuffElementUI b = Instantiate(GameManager.Resources.getUIElement("buffElement"), this.transform).GetComponent<BuffElementUI>();
			RectTransform rec = b.GetComponent<RectTransform>();
			rec.anchorMin = new Vector2(0f, 1f);
			rec.anchorMax = new Vector2(0f, 1f);

			rec.anchoredPosition = new Vector3(45 + (90 * x), -32 + (-64 * (int)(x / 4)));
			b.BuffNameText.text = name;
			b.BuffTimerText.text = time.ToString();

			x += 1;

			return b;
		}

		public void RemoveBuffElement (BuffElementUI element) {
			Destroy(element.gameObject);
			//elementCountX -= 1;
		}

		public void UpdateTimer (BuffElementUI element, int time) {
			element.BuffTimerText.text = time.ToString();
		}
	}
}