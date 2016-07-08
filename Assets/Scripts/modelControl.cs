using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class modelControl : MonoBehaviour {
	//Hold onto your butts. Here we go.

	char currentAxis;
	string currentMenu;
	char[] axisArray;
	int axisCounter;
	Text axisButtonText;
	Text menuButtonText;

	string[] menuModeArray;
	bool menuToggleBool;

	float rotationSpeed;
	float movementSpeed;
	float scaleSpeed;

	Color[] colorsArray;
	int colorCounter;

	Vector3 fwd;
	bool onObject;

	bool runScript;

	// Use this for initialization
	void Start () {
		// Declares a two-dimensional array to hold our various axes.
		axisArray = new char[] {'x', 'y', 'z'};
		currentAxis = axisArray[0];
		axisCounter = 0;

		menuModeArray = new string[] { "Rotation", "Movement" };
		currentMenu = menuModeArray [0];

		// Set our initial button text with a value of axisArray[0].
		// TODO: Clean this up.
		axisButtonText = GameObject.FindGameObjectWithTag ("Axis Button").GetComponentInChildren<Text> ();
		axisButtonText.text = currentAxis.ToString ().ToUpper ();

		menuButtonText = GameObject.FindGameObjectWithTag ("Move Mode Button").GetComponentInChildren<Text> ();
		menuButtonText.text = currentMenu;

		rotationSpeed = 3.3f;
		movementSpeed = 0.33f;
		scaleSpeed = 0.05f;

		colorsArray = new Color[] {  Color.white, Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.yellow };
		GetComponent<Renderer> ().material.color = colorsArray [0];


	}

	// Update is called once per frame
	void Update () {
		
	}

	public void changeAxis () {
		axisCounter++;

		if (axisCounter >= axisArray.Length) {
			axisCounter = 0;
		}

		currentAxis = axisArray [axisCounter];
		axisButtonText.text = currentAxis.ToString ().ToUpper ();
	}

	public void changeMoveMode() {

		// Toggles our Menu boolean value (you'll see the use below)
		menuToggleBool = !menuToggleBool;

		// I'm sorry / A hacky solution to set our menu mode based off of a boolean value.
		currentMenu = menuModeArray [menuToggleBool ? 1 : 0];
		menuButtonText.text = currentMenu;

		//Uncomment to log when the Move Mode is changed.
		//Debug.Log ("Menu changed to: " + currentMenu);
	}

	public void move (string direction) {
		if (currentMenu.Equals ("Rotation")) {
			if (direction.Equals ("+")) {
				switch (currentAxis) {
				case 'x':
					this.transform.Rotate (rotationSpeed, 0, 0, Space.Self);
					break;
				case 'y':
					// For some reason the y axis is backwards.
					this.transform.Rotate (0, -rotationSpeed, 0, Space.Self);
					break;
				case 'z':
					// For some reason the z axis is backwards.
					this.transform.Rotate (0, 0, -rotationSpeed, Space.Self);
					break;
				}
			} else if (direction.Equals ("-")) {
				switch (currentAxis) {
				case 'x':
					this.transform.Rotate (-rotationSpeed, 0, 0, Space.Self);
					break;
				case 'y':
					this.transform.Rotate (0, rotationSpeed, 0, Space.Self);
					break;
				case 'z':
					this.transform.Rotate (0, 0, rotationSpeed, Space.Self);
					break;
				}
			} else {
				Debug.Log ("Not a valid direction string for modelControl.interact()! Acceptable values are '+' or '-' (without quotations)");

			}
		} else if (currentMenu.Equals ("Movement")) {
			if (direction.Equals ("+")) {
				switch (currentAxis) {
				case 'x':
					this.transform.Translate (movementSpeed, 0, 0, Space.World);
					break;
				case 'y':
					this.transform.Translate (0, movementSpeed, 0, Space.World);
					break;
				case 'z':
					this.transform.Translate (0, 0, movementSpeed, Space.World);
					break;
				}
			} else if (direction.Equals ("-")) {
				switch (currentAxis) {
				case 'x':
					this.transform.Translate (-movementSpeed, 0, 0, Space.World);
					break;
				case 'y':
					this.transform.Translate (0, -movementSpeed, 0, Space.World);
					break;
				case 'z':
					this.transform.Translate (0, 0, -movementSpeed, Space.World);
					break;
				}
			} else { // If the input is neither '+' or '-'
				Debug.Log ("Not a valid direction string for modelControl.interact()! Acceptable values are '+' or '-' (without quotations)");

			}
		} else { // If the current Menu is not one specified above
			Debug.Log ("Not a valid Menu classification!");
		}
	}

	public void scale (string direction) {
		if (direction.Equals ("+")) {
			transform.localScale += new Vector3 (scaleSpeed, scaleSpeed, scaleSpeed);
		} else if (direction.Equals ("-")) {
			//if (((transform.localScale.x  - scaleSpeed) <= 0) || ((transform.localScale.y - scaleSpeed) <= 0) || ((transform.localScale.z - scaleSpeed) <= 0)) {
			//	transform.localScale = transform.localScale;
			//}
			//else {
				transform.localScale += new Vector3 (-scaleSpeed, -scaleSpeed, -scaleSpeed);
			//}
		}
		else {
			Debug.Log ("Not a valid direction string for modelControl.scale()! Acceptable values are '+' or '-' (without quotations)");
		}
	}

	public void changeColor (string direction) {
		if (direction.Equals ("+")) {
			if (colorCounter + 1 >= colorsArray.Length) {
				colorCounter = 0;
			} else {
				colorCounter += 1;
			}
			GetComponent<Renderer> ().material.color = colorsArray [colorCounter];
		}
		else if (direction.Equals ("-")) {
			if (colorCounter - 1 <= -1) {
				colorCounter = colorsArray.Length - 1;
			} else {
				colorCounter -= 1;
			}
			GetComponent<Renderer> ().material.color = colorsArray [colorCounter];
		}
	}
}