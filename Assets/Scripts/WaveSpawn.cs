using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour {

	public Transform[] enemyPrefab;
	public Transform spawnPoint;
	public int waveNumber = 0; 

	public Text waveCountdown;

	public float spawnWaitTime = 9.0f;
	public float countDown = 9.0f ;

	public Transform[] spawnedEnemies;


	private int levelChangeLimit = 3;


	void Update () {

		if(waveNumber < levelChangeLimit) { 
			if(countDown <= 0.0f) {
				StartCoroutine(nextWave ());
				countDown = spawnWaitTime;
			}
			countDown -= Time.deltaTime;

			waveCountdown.text = "Wave "+waveNumber.ToString ()+"\n"+Mathf.Floor(countDown).ToString ();
		}
		else {
			if(checkAllSpawnEnemyStatus () == true) {
				GameManager.instance.isLevelPassed = true;
				return;
			}
		}
	}

	IEnumerator nextWave() {

		waveNumber++;

		spawnedEnemies = new Transform[waveNumber];


		for(int i = 0; i< waveNumber; i++) {
			spawnEnemy(i); 
			yield return new WaitForSeconds (0.5f);
		}

	}

	bool checkAllSpawnEnemyStatus() {
		int check = 0;
		for (int i = 0; i < waveNumber; i++) {
			if(spawnedEnemies[i] == null) {
				check++;
			}
		}
		return (check == (waveNumber)) ? true : false;
	}

	void spawnEnemy(int eIndex) {
		int rIndex  = Random.Range (0, 2);
		spawnedEnemies[eIndex] = (Transform)Instantiate (enemyPrefab[rIndex], spawnPoint.position, enemyPrefab[rIndex].rotation);
	}
}
