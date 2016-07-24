using UnityEngine;
using System.Collections;

public class OrchirdSeed : Seed {

	// Use this for initialization
	override public void Start () {
		base.Start ();
		flowerPrefab = Resources.Load ("flowers/Orchid") as GameObject;
	}

	// Update is called once per frame
	override public void Update () {
		base.Update ();

	}
}
