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

	}
}
