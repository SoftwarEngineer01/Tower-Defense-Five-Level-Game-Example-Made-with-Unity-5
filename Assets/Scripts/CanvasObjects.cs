using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasObjects : MonoBehaviour
{
	private Color oldColor = Color.white;
	public GameObject lastObject = null;

	public void OnBottomButtonClicked(BaseEventData eventData) {
		if(lastObject != null) {
			lastObject.GetComponent<RawImage> ().color = oldColor;
		}
		lastObject = eventData.selectedObject;
		RawImage temp = lastObject.GetComponent<RawImage> ();

		temp.color = Color.cyan;
		if(lastObject.name == "TurretButton") {
			BuildManager.instance.defaultGameObjectToBuild = "Turret";
		}
		else if(lastObject.name == "MissileLauncherButton") {
			BuildManager.instance.defaultGameObjectToBuild = "MissileLauncher";
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
