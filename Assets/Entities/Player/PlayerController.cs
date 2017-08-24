using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Text healthText;
	public float health = 500f;
	public float speed = 15f;
	public float padding = 0.5f;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float firingRate = 0.2f;
	public LevelManager levelManager;

	float xmin;
	float xmax;

	void Start() {
		ScoreKeeper.Reset ();
		DefineScreenEdges ();
	}
		
	void Update () {
		UpdateHealthText ();
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

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}

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

	void Fire() {
		Vector3 offset = new Vector3 (0, 1, 0);
		GameObject beam = Instantiate (projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, projectileSpeed, 0);
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

		// Show the health
		UpdateHealthText();

		// Destroy laser
		projectile.Hit();

		// Check the remining health
		if (health <= 0) {
			Die ();
		}

	}

	void UpdateHealthText() {
		// Show the health
		healthText.text = health.ToString ();
	}

	void Die() {
		GameObject.Destroy (gameObject);
		levelManager.LoadLevel ("Win");
	}
}
