using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 15f;
	public float padding = 0.5f;

	float xmin;
	float xmax;

	void Start() {
		DefineScreenEdges ();
	}
		
	void Update () {
		ControleShip ();
	}

	void DefineScreenEdges () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));

		xmin = leftBoundary.x + padding;
		xmax = rightBoundary.x - padding;
	}

	void ControleShip() {

		if (Input.GetKey (KeyCode.LeftArrow)) {
			//transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		/*if (Input.GetKey (KeyCode.UpArrow)) {
			//transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
			transform.position += Vector3.up * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			//transform.position += new Vector3(0f, -speed * Time.deltaTime, 0f);
			transform.position += Vector3.down * speed * Time.deltaTime;
		}*/

		// Restrict the player to the game space
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}
}
