using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Awake() {

		if(instance != null){
			Debug.Log ("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}


	public string defaultGameObjectToBuild = "Turret";

	private GameObject turretToBuild;
	private GameObject missileLauncherToBuild;

	[Header("Standart Prefabs")]
	public GameObject standartTurretPrefab;
	public GameObject standartMissileLauncherPrefab;

	public GameObject GetTurretBuild() {
		GameManager.instance.userPoints -= 20;
		return turretToBuild;
	}

	public GameObject GetMissileLauncherBuild() {
		GameManager.instance.userPoints -= 30;
		return missileLauncherToBuild;
	}

	// Use this for initialization
	void Start () {
		turretToBuild = standartTurretPrefab;
		missileLauncherToBuild = standartMissileLauncherPrefab;
	}

}
