using UnityEngine;

public class MissileLauncher : MonoBehaviour {

	private Transform target = null;

	[Header("Attributes")]
	public float range = 15f;
	private float fireCountDown = 0f;
	public float fireRate = 1f;

	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";

	public Transform partToRotate;

	[Header("Bullet Properties")]

	public GameObject bulletPrefab;

	public Transform firePoint;

	// Use this for initialization
	void Start() {
		InvokeRepeating ("UpdateTarget",0.0f, 1.0f);

	}

	void UpdateTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);

		float shortestDistance = Mathf.Infinity;

		GameObject nearestEnemyObject = null;
		foreach(GameObject sEnemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, sEnemy.transform.position);

			if(distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemyObject = sEnemy;
			}
		}

		if(nearestEnemyObject != null && shortestDistance <= range) {
			target = nearestEnemyObject.transform;
		}
		else {
			target = null;
		}
	}

	void Shoot() {
		GameObject bulletGo = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation); 
		Bullet bullet = bulletGo.GetComponent<Bullet> ();


		if(target != null) {
			bullet.Seek (target);
		}

		GetComponent<AudioSource> ().Play ();

		GameManager.instance.gainPoints (2);
	}

	// Update is called once per frame
	void Update () {
		if (target == null)
			return; 
		//Target Lock On
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = lookRotation.eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f); 

		if(fireCountDown <= 0f) {
			Shoot ();
			fireCountDown = 1f / fireRate;
		}

		fireCountDown -= Time.deltaTime;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
