using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class uploaderControl : MonoBehaviour {

	public string baseURL;
	// Use this for initialization
	void Start () {
		baseURL = "http://bryson.xyz/";
		Destroy(GameObject.Find("Error Message"));
		//modelButton = Instantiate (Resources.Load ("Prefabs/modelButton")) as GameObject;
		//modelButton.transform.SetParent (transform, false);

		WWW fileTXT = new WWW(baseURL + "/models.txt");
		while (!fileTXT.isDone) {
			Debug.Log ("Download Progress: " + ((fileTXT.bytesDownloaded  / fileTXT.size) * 100) + "%");
		}
		//Debug.Log (fileTXT.text);
		//string[] str = fileTXT.text.Where(function(c) { c != "\n"[0]}).Select( function(c){} int.Parse(c.ToString())}).ToArray();
		//Debug.Log(str.toString());
		string[] lines = fileTXT.text.Split("\n" [0]);
		for (int i = 0; i < lines.Length; i++) {
			if (!string.IsNullOrEmpty(lines[i])) {
				//Debug.Log (lines [i]);
				buildModel (lines[i]);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	GameObject buildModel(string fileName) {
		fileName = fileName.Replace ("\n", "");
		fileName = fileName.Replace ("\r", "");
		//fileName = fileName.Replace (" ", "%20");
		string modelName = fileName.Replace (".obj", "");
		//modelName = modelName.Replace(modelName.Substring(0,1), modelName.Substring(0,1).ToUpper());
		GameObject modelButton = Instantiate (Resources.Load ("Prefabs/modelButton")) as GameObject;
		modelButton.name = modelName;
		modelButton.GetComponentInChildren<Text> ().text = modelName;
		modelButton.transform.SetParent (transform, false);
		modelButton.GetComponent<Button> ().onClick.AddListener (() => { buildMesh(fileName); });
		return modelButton;
	}

	void buildMesh(string fileW) {

		WebClient client = new WebClient();
		fileW = fileW.ToLower ();
		WWW modelOBJ = new WWW(baseURL + "/" + fileW);

		while (!modelOBJ.isDone) {
			Debug.Log ("Progress: " + modelOBJ.bytesDownloaded + "/" + modelOBJ.size);
		}

		File.WriteAllBytes(Application.persistentDataPath + fileW, modelOBJ.bytes);

		FastObjImporter objImporter = new FastObjImporter();

		Mesh modelMesh = objImporter.ImportFile(Application.persistentDataPath + fileW);
		GameObject.FindGameObjectWithTag ("User Model").GetComponent<MeshFilter> ().mesh = modelMesh;
		GameObject.FindGameObjectWithTag ("User Model").GetComponent<MeshCollider> ().sharedMesh = modelMesh;

		Debug.Log ("Mesh Size: " + modelMesh.bounds.size.ToString ());

		scaleMesh ();
	}

	void scaleMesh () {
		Mesh mesh = GameObject.FindGameObjectWithTag ("User Model").GetComponent<MeshFilter> ().mesh;
		Vector3 modelScale = GameObject.FindGameObjectWithTag ("User Model").transform.localScale;
		//mesh.bounds.size.Set (modelScale.x, modelScale.y, modelScale.z);
		//mesh.RecalculateBounds ();

		float newSizeRatioX = modelScale.x / mesh.bounds.size.x;
		float newSizeRatioY = modelScale.y / mesh.bounds.size.y;
		float newSizeRatioZ = modelScale.z / mesh.bounds.size.z;

		float minimumNewSizeRatio = Mathf.Min (newSizeRatioX, Mathf.Min (newSizeRatioY, newSizeRatioZ));

		Vector3 newScale = GameObject.FindGameObjectWithTag ("User Model").transform.localScale * minimumNewSizeRatio;
		GameObject.FindGameObjectWithTag ("User Model").transform.localScale = newScale;
		//mesh.bounds.size.Set (newScale.x, newScale.y, newScale.z);
	}
}
