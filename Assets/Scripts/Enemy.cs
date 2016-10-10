using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float speed = 10f;

	private Transform target;

	private Vector3 dir;

	private int wavePointIndex = 0;

	void Start() {
		target = Waypoints.points [0];
	}

	void Update() {
		dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if(Vector3.Distance(transform.position,target.position)<=0.2f) {
			GetNextWaypoint ();
		}
	}

	void GetNextWaypoint() {

		if(wavePointIndex >= Waypoints.points.Length - 1) {
			Destroy (gameObject);

			GameManager.instance.changeHealthBar ();

			return;
		}
		wavePointIndex++;

		target = Waypoints.points [wavePointIndex];

		dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = lookRotation.eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f); 
	}
}
