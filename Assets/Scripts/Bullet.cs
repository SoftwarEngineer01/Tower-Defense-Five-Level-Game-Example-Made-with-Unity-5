using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private Transform target;

	public float speed = 70f;

	public GameObject impactEffect;


	// Use this for initialization
	public void Seek (Transform _target) {
		target = _target;
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null) {
			DestroyObject (gameObject);
			return;
		}
		else {
			Vector3 dir = target.position - transform.position;
			float distanceThisFrame = speed * Time.deltaTime;

			if(dir.magnitude <= distanceThisFrame) {
				HitTarget ();
				return;
			}

			transform.Translate(dir.normalized * distanceThisFrame, Space.World); 
		}
	}

	void HitTarget() {

		GameObject effectIns = (GameObject)Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (effectIns, 2f);
		DestroyObject (gameObject);
		Destroy (target.gameObject);
	}
}
