using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float Dammage = 100f;

	public void Hit() {
		Destroy (gameObject);
	}
}
