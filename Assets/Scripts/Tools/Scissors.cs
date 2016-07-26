using UnityEngine;
using System.Collections;

public class Scissors : MonoBehaviour {
	private Vector3 originalPosition;
	private Collider[] cols;

	private Transform flowers;

	public bool held = false;


	void Start () {
		originalPosition = this.transform.position;
		cols = this.GetComponents<Collider> ();
//		flowers = GameObject.Find ("Flowers").transform;
	}

	private void ResetCan() {
		this.transform.position = originalPosition;
		this.transform.eulerAngles = new Vector3 (0f, 175f, 0f); 
		this.GetComponent<Rigidbody> ().isKinematic = true;
		foreach (Collider c in cols) {
			c.isTrigger = true;
		}
		held = false;
		this.GetComponent<Animator> ().SetBool ("cutting", false);
		this.GetComponent<AudioSource> ().Stop();

	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "soil" || col.gameObject.tag == "Player") {
			ResetCan ();
		}
		if (col.gameObject.tag == "stem") {
			col.gameObject.GetComponent<Flower> ().Cut ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "hand") {
			this.GetComponent<Rigidbody> ().isKinematic = false;
			cols [0].isTrigger = false;
			held = true;
			this.GetComponent<Animator> ().SetBool ("cutting", true);
			this.GetComponent<AudioSource> ().Play ();
		}

		if (col.gameObject.tag == "soil" || col.gameObject.tag == "Player") {
			ResetCan ();
		}
		if (col.gameObject.tag == "stem") {
			col.GetComponent<Flower> ().Cut ();
		}
	}

	void OnCollisionStay(Collision col) {
		if (col.gameObject.tag == "hand") {
			held = true;
			this.GetComponent<Animator> ().SetBool ("cutting", true);
			this.GetComponent<AudioSource> ().Play ();
		}
	}

	void Update() {
		this.transform.eulerAngles = new Vector3 (0f, 180f, 0f); 
		Vector3 pos = this.transform.position;
		if ((pos.x >= 6.5f || pos.x <= -5.5f) || (pos.y >= 4.2f || pos.y <= -3.8f)) {
			ResetCan ();
		}
	}
}
