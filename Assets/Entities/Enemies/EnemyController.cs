using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float health = 100f;
	public GameObject projectile;
	public float projectileSpeed = 1f;
	public float shotsPerSeconds = 0.5f;

	void Update() {
		float probabilityOfFiring = Time.deltaTime * shotsPerSeconds;

		if (Random.value < probabilityOfFiring) {
			Fire ();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		// Get The laser object
		GameObject laser = collider.gameObject;
		// Get the projectile
		Projectile projectile = laser.GetComponent<Projectile> ();

		// If not a projectile, return
		if (!projectile) {
			return;
		}

		// Remove Health
		health -= projectile.Dammage;

		// Destroy laser
		projectile.Hit();

		// Check the remining health
		if (health <= 0) {
			GameObject.Destroy (gameObject);
		}

	}

	void Fire() {
		Vector3 startPositon = transform.position + new Vector3 (0, -1, 0);
		GameObject missile = Instantiate (projectile, startPositon, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -projectileSpeed);
	}

}
