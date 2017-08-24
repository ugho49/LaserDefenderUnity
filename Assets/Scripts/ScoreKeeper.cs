using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private static int score = 0;

	void Update() {
		gameObject.GetComponent<Text> ().text = score.ToString ();
	}

	public static void AddPoints(int points) {
		score += points;
	}

	public static void Reset() {
		score = 0;
	}
}
