using UnityEngine;
using System.Collections;

public class DandelionSeed : Seed {

	// Use this for initialization
	override public void Start () {
		base.Start ();
		flowerPrefab = Resources.Load ("flowers/Dandelion") as GameObject;
	}

	// Update is called once per frame
	override public void Update () {
		base.Update ();
		Vector3 pos = this.transform.position;

		if ((pos.x >= 7f || pos.x <= -7f) || pos.y <= -4.7) {
			Destroy(this.gameObject);
		}
	}
}
