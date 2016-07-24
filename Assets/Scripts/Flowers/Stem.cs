using UnityEngine;
using System.Collections;

public class Stem : MonoBehaviour {

	public GameObject flower;
	private bool cut = false;

	void Start() {
		flower = this.transform.parent.parent.gameObject;
	}

	public void Cut() {
		if (!cut) {
			flower.GetComponent<Flower> ().Cut ();
			cut = true;
		}
	}
}
