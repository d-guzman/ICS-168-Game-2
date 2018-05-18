using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

	public playerHealth healthController;

	private Slider healthBar;

	// may want to use for later, when we start assigning multiple players
	void awake() {}

	// public RectTransform healthBar;

	// Use this for initialization
	void Start () {
		// if (healthBar == null) {
		// 	healthBar = GetComponent<RectTransform>();
		// }

		healthBar = GetComponent<Slider>();
		healthBar.maxValue = healthController.health;
	}
	
	// Update is called once per frame
	void Update () {
		// healthBar.sizeDelta = new Vector2(healthController.health * 2, healthBar.sizeDelta.y);
        // healthBar.localPosition.x = (float) health;
		healthBar.value = healthController.health;
	}
}
