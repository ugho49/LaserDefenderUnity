using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float cameraHeight = 2f * Camera.main.orthographicSize;
		float cameraWidth = cameraHeight * Camera.main.aspect;

		// Set the size of the collider
		Vector2 colliderSize = new Vector2 (0, 1);
		colliderSize.x = cameraWidth;
		GetComponent<BoxCollider2D> ().size = colliderSize;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject.Destroy (collider.gameObject);
	}
}
