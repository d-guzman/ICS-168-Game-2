using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayController : MonoBehaviour {

	public HealthBarController scoreInfo;

	[Tooltip("This variable does not need to be assigned necessarily in the inspector. However, if it isn't, then scoreInfo needs to have a value.")]
	public playerHealth scoreController;

	// obsolete
	// private int personalScore;

	private int scoreNeeded;

	private Text scoreDisplay;

	// Use this for initialization
	void Start () {
		scoreDisplay = gameObject.GetComponent<Text>();
		scoreNeeded = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().winCondition;

		if (scoreController == null) {
			scoreController = scoreInfo.healthController;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreController == null) {
			scoreController = scoreInfo.healthController;
		}
		scoreDisplay.text = "Kills: " + scoreController.killCount + " / " + scoreNeeded;
	}
}
