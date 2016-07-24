using UnityEngine;
using System.Collections;

public class RoseSeed :  Seed {

	// Use this for initialization
	override public void Start () {
		base.Start ();
		flowerPrefab = Resources.Load ("flowers/Rose") as GameObject;
	}

	// Update is called once per frame
	override public void Update () {
		base.Update ();

	}
}
