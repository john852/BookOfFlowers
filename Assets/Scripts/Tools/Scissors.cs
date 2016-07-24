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
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "soil" || col.gameObject.tag == "Player") {
			ResetCan ();
		}
		if (col.gameObject.tag == "stem") {
			Debug.Log ("cutting stem");
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "hand") {
			this.GetComponent<Rigidbody> ().isKinematic = false;
			cols [0].isTrigger = false;
			held = true;
			this.GetComponent<Animator> ().SetBool ("cutting", true);
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
		}
	}

	void Update() {
		this.transform.eulerAngles = new Vector3 (0f, 180f, 0f); 
	}
}
