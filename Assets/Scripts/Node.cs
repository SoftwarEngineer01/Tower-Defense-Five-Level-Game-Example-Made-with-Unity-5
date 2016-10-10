using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {


	[Header("Build Object")]
	private GameObject building;

	[Header("Properties")]
	private Color hoverColor;
	private Color startColor;
	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
		hoverColor = Color.gray;
	}

	void OnMouseDown() {
		if(building != null){
			Debug.Log ("You can't build here! There is a building in here!");
			return;
		}
		else {
			if((BuildManager.instance.defaultGameObjectToBuild == "Turret" && GameManager.instance.userPoints < 20)||(BuildManager.instance.defaultGameObjectToBuild == "MissileLauncher" && GameManager.instance.userPoints <30)) {
				Debug.Log ("You don't have enough points to build here!");
				return;
			}
			else {
				GameObject buildObjectToBuild = null;

				if (BuildManager.instance.defaultGameObjectToBuild == "Turret") {
					buildObjectToBuild = BuildManager.instance.GetTurretBuild ();
				}
				else if (BuildManager.instance.defaultGameObjectToBuild == "MissileLauncher") {
					buildObjectToBuild = BuildManager.instance.GetMissileLauncherBuild ();
				} 
				building = (GameObject)Instantiate (buildObjectToBuild, transform.position, transform.rotation);
				GameManager.instance.changeScoreBoards ();
			}
		}
	}

	void OnMouseEnter() {
		rend.material.color = hoverColor;
	}

	void OnMouseExit() {
		rend.material.color = startColor;
	}
}
