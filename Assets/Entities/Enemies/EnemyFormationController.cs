using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float spawnDelay = 0.2f;
	public float speed = 3f;
	public float width = 10f;
	public float height = 5f;

	bool movingRight = true;
	float xmin;
	float xmax;

	void Start () {
		DefineScreenEdges ();
		SpawnEnemies ();
	}
		
	void Update () {
		HandleFormationMovement ();

		if (AllMembersDead ()) {
			SpawnEnemies ();
		}
	}

	void DefineScreenEdges () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));

		xmin = leftBoundary.x;
		xmax = rightBoundary.x;
	}

	/*void SpawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}*/

	void SpawnEnemies() {
		Transform freePosition = NextFreePosition ();

		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		if (NextFreePosition ()) {
			Invoke ("SpawnEnemies", spawnDelay);
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}

	void HandleFormationMovement() {

		float leftEdgeOfFormation = transform.position.x - (width * 0.5f);
		float rightEdgeOfFormation = transform.position.x + (width * 0.5f);

		if (leftEdgeOfFormation <= xmin) {
			movingRight = true;
		} else if (rightEdgeOfFormation >= xmax) {
			movingRight = false;
		}

		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	bool AllMembersDead () {
		foreach (Transform child in transform) {
			if (child.childCount > 0) {
				return false;
			}
		}

		return true;
	}

	Transform NextFreePosition() {
		foreach (Transform child in transform) {
			if (child.childCount == 0) {
				return child;
			}
		}

		return null;
	}
}
