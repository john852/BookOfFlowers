using UnityEngine;
using System.Collections;

public class Petals : MonoBehaviour {

	protected bool fallen = false;
	protected float rottenTime;
	protected float time;
	private SpriteRenderer sr;


//
//	virtual public void OnTriggerStay(Collider other) {
//		if (other.gameObject.tag == "hand") {
//			StartRotting ();
//		}
//	}
//
//	virtual public void OnTriggerExit(Collider other) {
//	}

	public void StartRotting() {
		if (!fallen) {
			Collider[] col = this.GetComponents<Collider> ();

			if (col.Length > 1) {

				col [0].isTrigger = false;
				col [1].isTrigger = false;
			} else {
				col [0].isTrigger = false;
			}

			this.GetComponent<Rigidbody> ().isKinematic = false;
			this.GetComponent<Rigidbody> ().useGravity = true;

			fallen = true;
			time = Time.deltaTime;
			rottenTime = Random.Range (30f, 70f);
			sr = this.GetComponent<SpriteRenderer> ();
		}
	}

	virtual public void Update() {
		if (fallen) {

			time += Time.deltaTime;

			float blue, red, green, a;
			red = Mathf.Lerp (255f, 39f, (time / rottenTime)) / 255f;
			green = Mathf.Lerp (255f, 26f, (time / rottenTime)) / 255f;
			blue = Mathf.Lerp (255f, 26f, (time / rottenTime)) / 255f;
			a = Mathf.Lerp (255f, 50f, (time / rottenTime)) / 255f;

			sr.color = new Color (red, green, blue, a);

			if (a <= 51f/255f)
				Destroy (this.gameObject);
		}
	}
}
