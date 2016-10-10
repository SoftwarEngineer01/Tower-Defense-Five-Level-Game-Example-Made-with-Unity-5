using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public static GameManager instance;

	public Scrollbar gameCanvasScroolBar;
	private float healthValue = 1f;

	public static int totalPoints = 0;

	public int userPoints = 30;
	private float oldTimeScale;
	public float panSpeed = 30f;

	private bool isStatusBusy = false;

	public Transform camPosition;

	[Header("Game Sounds")]
	public AudioClip gameIsOverSound;
	public AudioClip levelPassedSound;
	public AudioClip gameFinishedSound;

	[Header("Situations")]
	public bool isLevelPassed = false;
	private bool isPaused = false;
	private bool isDead = false;

	[Header("Gui Properties")]
	public Texture2D boxTexture;
	public Texture2D pauseTexture;
	public Texture2D levelPassedTexture;
	public Texture2D gameFinishedTexture;
	public Text pointsText;


	void Awake() {

		if(instance != null){
			Debug.Log ("More than one BuildManager in scene!");
			return;
		}
		instance = this;
		oldTimeScale = Time.timeScale;
		isStatusBusy = false;
	}

	public void gainPoints(int point) {
		userPoints += point;
		GameManager.totalPoints += point;
		changeScoreBoards ();
	}

	public void changeScoreBoards() {
		pointsText.text = "Points : " + userPoints.ToString ();
	}

	void Update() {
		if (Input.GetKeyDown ("escape")) {
			if (isPaused == true) {
				isPaused = false;
				Time.timeScale = oldTimeScale;
			} else {
				isPaused = true;
				Time.timeScale = 0f;
			}

		}

		if (Input.GetKey (KeyCode.W)) {
			camPosition.transform.Translate (Vector3.forward * panSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			camPosition.transform.Translate (Vector3.back * panSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			camPosition.transform.Translate (Vector3.left * panSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			camPosition.transform.Translate (Vector3.right * panSpeed * Time.deltaTime);
		}
	}

	public void changeHealthBar () {
		if(isDead == true) {
			return;
		}

		if(healthValue > 0.0f) {
			gameCanvasScroolBar.size  -= 0.1000000f;
			healthValue -= 0.1000000f;
		}
		else {
			healthValue = 0.0f;
			gameCanvasScroolBar.size  = 0f;
			gameCanvasScroolBar.interactable = false;
			isDead = true;
		}
	}

	public IEnumerator EndGame() {

		yield return new WaitForSeconds (1.9f);

		isStatusBusy = false;

		Application.LoadLevel ("MainMenu");
	}

	public IEnumerator FinishGame() {

		yield return new WaitForSeconds (3f);

		isStatusBusy = false;

		Application.LoadLevel ("MainMenu");
	}

	public IEnumerator PassLevel() {

		yield return new WaitForSeconds (1.9f);

		isStatusBusy = false;

		Application.LoadLevel (Application.loadedLevel + 1);
	}


	void OnGUI()
	{ 
		if(isPaused == true) {
			GUIStyle myStyle = new GUIStyle();
			myStyle.normal.background = pauseTexture;
			Rect R1 = new Rect ((Screen.width / 2f)-135f, (Screen.height / 2)-31f, 270, 63);

			GUI.Box(R1, "", myStyle);

			isStatusBusy = true;
		}

		if(isDead == true) {
			GUIStyle myStyle = new GUIStyle();
			myStyle.normal.background = boxTexture;
			Rect R1 = new Rect ((Screen.width / 2f)-135f, (Screen.height / 2)-31f, 270, 63);

			GUI.Box(R1, "", myStyle);

			if(isStatusBusy == false) {
				StartCoroutine (EndGame());

				AudioSource audio = GetComponent<AudioSource>();
				audio.clip = gameIsOverSound;
				audio.Play();
			}

			isStatusBusy = true;
		}

		if(isLevelPassed == true) {
			GUIStyle myStyle = new GUIStyle();
			Rect R1 = new Rect ((Screen.width / 2f)-135f, (Screen.height / 2)-31f, 270, 63);


			if((Application.loadedLevel + 2) >= Application.levelCount) {
				myStyle.normal.background = gameFinishedTexture;
				GUI.Box(R1, "", myStyle);
				GUIStyle myStyle2 = new GUIStyle();
				myStyle2.richText = true;
				myStyle2.fontSize = 15;
				GUI.Box(new Rect ((Screen.width / 2f)-80f, (Screen.height / 2)+31f, 150, 24), "<b>Your total point is " + GameManager.totalPoints.ToString ()+"</b>");

				if(isStatusBusy == false) {
					StartCoroutine (FinishGame());

					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = gameFinishedSound;
					audio.Play();
				}
				isStatusBusy = true;
			}
			else {
				
				myStyle.normal.background = levelPassedTexture;

				GUI.Box(R1, "", myStyle);


				if(isStatusBusy == false) {
					StartCoroutine (PassLevel());

					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = levelPassedSound;
					audio.Play();
				}
				isStatusBusy = true;
			}
		}

	}
}
