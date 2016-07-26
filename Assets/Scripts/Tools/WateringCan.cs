using UnityEngine;
using System.Collections;

public class WateringCan : MonoBehaviour {
	private Vector3 originalPosition;
	private Collider[] cols;

	private GameObject waterPouring;
	private Transform flowers;

	private Transform waterPos;

	private ParticleSystem.EmissionModule em;
	private ParticleSystem.MinMaxCurve rate;

	public bool pouring = false;
	public bool waterPour = false;
	private bool soundOn = false;


	void Start () {
		originalPosition = this.transform.position;
		cols = this.GetComponents<Collider> ();
		waterPouring = GameObject.Find ("Water");
		flowers = GameObject.Find ("Flowers").transform;
		waterPos = this.transform.GetChild (0);
		waterPouring.SetActive(false);
		soundOn = false;

		em = waterPouring.GetComponent<ParticleSystem> ().emission;
		rate = em.rate;
		// Set the Mode to Constant.
		rate.mode = ParticleSystemCurveMode.Constant;
	}

	private void ResetCan() {
		this.transform.position = originalPosition;
		pouring = false;
		this.transform.eulerAngles = new Vector3 (0f, 175f, 0f); 
		this.GetComponent<Rigidbody> ().isKinematic = true;
		foreach (Collider c in cols) {
			c.isTrigger = true;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "soil" || col.gameObject.tag == "Player") {
			ResetCan ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "hand" && !pouring) {
			this.GetComponent<Rigidbody> ().isKinematic = false;
			cols [0].isTrigger = false;
			pouring = true;
//			this.GetComponent<Collider> ().isTrigger = false;
		}

		if (col.gameObject.tag == "soil" || col.gameObject.tag == "Player") {
			ResetCan ();
		}
	}
	private float stopTime = 0f;
	IEnumerator StopPouring() {
		yield return new WaitForSeconds (.02f);
		stopTime += .02f;

		float temp = waterPouring.GetComponent<ParticleSystem> ().emission.rate.constantMax;

		rate = em.rate;
		rate.constantMin = temp-10f;
		rate.constantMax = temp-10f;
		this.GetComponent<AudioSource> ().volume -= .05f;
		em.rate = rate;

		if (stopTime >= (0.02f * 10f * 2.4f)) {
			waterPouring.SetActive (false);
			soundOn = false;
			this.GetComponent<AudioSource> ().Stop ();
			foreach (Transform child in flowers) {
				child.GetComponent<Flower> ().watering = false;
			}
		} else {
			StartCoroutine(StopPouring());
		}
			
	}

	void Update() {
		Vector3 angles = this.transform.eulerAngles;
		Vector3 pos = this.transform.position;

		float degree = angles.z;
		if (pouring && (degree >= 297 && degree <= 340)) {
			waterPour = true;
			waterPouring.SetActive (true);
			this.GetComponent<AudioSource> ().volume = .6f;
			if (!soundOn) {
				this.GetComponent<AudioSource> ().Play ();
				soundOn = true;
			}
			rate = em.rate;
			rate.constantMin = 100f;
			rate.constantMax = 100f;
			em.rate = rate;
			stopTime = 0f;

			foreach (Transform child in flowers) {
				child.GetComponent<Flower> ().watering = true;
			}
		} else if (waterPour) {
			waterPour = false;

			rate = em.rate;
			rate.constantMin = 100f;
			rate.constantMax = 100f;
			em.rate = rate;
			StopAllCoroutines ();
			stopTime = 0f;
			StartCoroutine (StopPouring());
		}
		if (waterPour) {
			waterPouring.transform.position = waterPos.position;
		}

		if (angles.x > 10 && angles.x <= 330) {
			this.transform.eulerAngles = new Vector3 (0f, 175f, 0f); 
		}

		if ((pos.x >= 6.5f || pos.x <= -7f) || (pos.y >= 4.2f || pos.y <= -3.8f)) {
			ResetCan ();
		}
	}
}
