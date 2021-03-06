using UnityEngine;
using System.Collections;

public class uiRotation : MonoBehaviour {

	// Variable declarations
	bool onObject;
	GameObject canvasObject;
	GameObject cameraObject;
	Quaternion rot;
	Vector3 euler;
	Vector3 fwd;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		// Our UI canvas object needs to be tagged with the "UI Canvas" tag to be able to be located.
		canvasObject = GameObject.FindGameObjectWithTag ("UI Canvas");
		// Our user, since we don't know where the initial model is located, is not looking at anything with a Box Collider.
		onObject = false;
	}

	// Update is called once per frame
	void Update () {
		// Get the rotation of our camera in degrees.
		euler = transform.rotation.eulerAngles;
		// Convert these angles to a Quaternion object.
		rot = Quaternion.Euler(canvasObject.transform.eulerAngles.x, euler.y, 0); 

		// If our user is not looking at an object with a Box Collider, rotate the UI Canvas with the camera.
		// This ensures that our UI doesn't move while the user is looking at it.
		if (!onObject) {
			canvasObject.transform.rotation = rot;
		}

		if ((onObject && (hit.collider.tag.Equals("User Model"))) && Input.GetMouseButtonDown(0)) {
			//Debug.Log ("Hit User Model at: " + hit.point.x + ", " + hit.point.y + ", " + hit.point.z);
			if (GameObject.FindGameObjectWithTag ("Pointer")) {
				Destroy (GameObject.FindGameObjectWithTag ("Pointer"));
			}

			GameObject pointer = (GameObject)Instantiate(Resources.Load("Prefabs/Pointer"), hit.point, Quaternion.identity);
			//pointer.transform.Translate(0, -0.0375f, 0);
			pointer.name = "Pointer";
			pointer.GetComponent<Renderer>().material.color = Color.red;
			pointer.transform.parent = GameObject.FindGameObjectWithTag ("User Model").transform;;
		}
	}

	void FixedUpdate() {

		// Declare and update our Ray object as a Vector3
		fwd = transform.TransformDirection (Vector3.forward);
		//RaycastHit hit;
		// If our Raycast hits an object with a Box Collider, update our onObject variable.
		if (Physics.Raycast (transform.position, fwd, out hit, 10)) {
			onObject = true;
		} else {
			onObject = false;
		}
	}
		
}
