using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void StartGame() {
		Application.LoadLevel ("Level01");
	}

	public void ExitGame() {
		Application.Quit ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
