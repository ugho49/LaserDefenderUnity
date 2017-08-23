using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
			// Duplicate music player, self-destructing"
			return;
		} 

		instance = this;
		GameObject.DontDestroyOnLoad (gameObject);
	}

}
