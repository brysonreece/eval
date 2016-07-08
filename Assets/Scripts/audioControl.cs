using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class audioControl : MonoBehaviour {
	
	AudioSource audioComponent;
	bool muteMusic;

	void Start() {
		
		audioComponent = this.GetComponent<AudioSource>();
		muteMusic = false;

	}

	public void ToggleMusic() {
		muteMusic = !muteMusic;

		if (muteMusic) {
			audioComponent.mute = true;
		}
		else {
			audioComponent.mute = false;
		}
	}

	void Update() {

	}
}