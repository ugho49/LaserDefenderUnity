using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float speed = 3f;
	public float width = 10f;
	public float height = 5f;
	public float y = 0f;

	bool movingRight = true;
	float xmin;
	float xmax;

	void Start () {
		DefineScreenEdges ();
		InitEnemyFormation ();
	}
		
	void Update () {
		HandleFormationMovement ();
	}

	void DefineScreenEdges () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));

		xmin = leftBoundary.x;
		xmax = rightBoundary.x;
	}

	void InitEnemyFormation() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	public void OnDrawGizmos() {
		Vector3 pos = transform.position;
		pos.y = y;

		Gizmos.DrawWireCube(pos, new Vector3(width, height, 0));
	}

	void HandleFormationMovement() {

		float leftEdgeOfFormation = transform.position.x - (width * 0.5f);
		float rightEdgeOfFormation = transform.position.x + (width * 0.5f);

		if (leftEdgeOfFormation <= xmin || rightEdgeOfFormation >= xmax) {
			movingRight = !movingRight;
		}

		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}
}
