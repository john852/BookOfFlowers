using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour {

	protected bool disintegrating = false;

	protected Material mat;
	protected float time;
	protected float disintegrationLength;
	protected GameObject flowers;
	protected bool flo;

	protected GameObject flowerPrefab;

	protected Vector3 originalPosition;


	virtual public void Start () {
		mat = this.GetComponent<Renderer> ().material;
		disintegrationLength = Random.Range (1f, 3f);
		flowers = GameObject.Find ("Flowers");
		originalPosition = this.transform.position;
	}

	protected float rotX, rotY, rotZ;
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "soil") {
			disintegrating = true;
			this.GetComponent<Collider> ().enabled = false;
			this.GetComponent<Rigidbody> ().isKinematic = true;
			time = Time.deltaTime;

			rotX = this.transform.eulerAngles.x;
			rotY = this.transform.eulerAngles.y;
			rotZ = this.transform.eulerAngles.z;
		}

		if (col.gameObject.tag == "Player") {
			this.GetComponent<Rigidbody> ().isKinematic = true;
			this.GetComponent<Collider> ().isTrigger = true;

			this.transform.position = originalPosition;
		}
			
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "hand") {
			this.GetComponent<Rigidbody> ().isKinematic = false;
			this.GetComponent<Collider> ().isTrigger = false;
		}
			
	}

//	void OnCollisionExit(Collision col) {
//	}

	// Update is called once per frame
	virtual public void Update () {
		if (disintegrating) {
			time += Time.deltaTime;

			float red, green, blue, x, y, z;
			red = Mathf.Lerp (255f, 60, (time / disintegrationLength)) / 255f;
			green = Mathf.Lerp (255f, 45, (time / disintegrationLength)) / 255f;
			blue = Mathf.Lerp (255f, 38, (time / disintegrationLength)) / 255f;

			mat.color = new Color (red, green, blue);

			x = Mathf.Lerp (rotX, 0f, (time / disintegrationLength));
			y = Mathf.Lerp (rotY, 90f, (time / disintegrationLength));
			z = Mathf.Lerp (rotZ, 0f, (time / disintegrationLength));

			this.transform.eulerAngles = new Vector3 (x, y, z);

			if (time > disintegrationLength) {
				this.transform.Translate (0f, -0.005f, 0f);
			}
			if (time > disintegrationLength + 1f && !flo && (flowers.transform.childCount <= 25)) {
				flo = true;
				GameObject flower = Instantiate (flowerPrefab, new Vector3 (this.transform.position.x, this.transform.position.y, 0f), Quaternion.identity) as GameObject;
				flower.transform.SetParent (flowers.transform, false);
				Destroy (this.gameObject);
			} else if  (flowers.transform.childCount > 20) {
				Destroy (this.gameObject);
			}

		}


	}
}
