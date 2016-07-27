using UnityEngine;
using System.Collections;

public class Orchid : Flower {

	private int leaveNum = 2;
	// Use this for initialization
	override public void Start () {
		minGrowthLength = 12f * 60f* growthFactor;
		maxGrowthLength = 18f * 60f* growthFactor;
		base.Start ();
		seedPrefab = Resources.Load("Seeds/OrchidSeed") as GameObject;
		leaves = this.transform.GetChild (1).GetChild(2);
	}

	override public IEnumerator Decay() {
		decayTime = Random.Range (((1f/4f)*growthLength)/34f, ((1f/4f)*growthLength)/30f);
		yield return new WaitForSeconds (decayTime);

		int ran = Random.Range (0, 4);

		if (leaveNum > 0 && ran < 1) {
			leaveNum--;
			Transform leave = leaves.GetChild (Random.Range (0, leaves.transform.childCount));
			leave.GetComponent<DandelionLeave> ().StartDecay();
		}

		Transform petals = flowerbody.transform.GetChild (1);
		if (petals.childCount == 1) {
			petals.GetChild (0).GetComponent<Petals> ().StartRotting ();
		} else if (petals.childCount > 1) {
			petals.GetChild (Random.Range(0, petals.transform.childCount)).GetComponent<Petals> ().StartRotting ();
			StartCoroutine (Decay());
		}

	}


	// Update is called once per frame
	override public void Update () {
		base.Update ();

		if (!flowerbody.GetComponent<FlowerBody> ().active && !flowerbody.GetComponent<FlowerBody> ().decay && time >= ((1f / 5f) * growthLength) && !cut) {
			SetFlowerBodyActive ();
		}

		if (time >= (growthLength * 1.5f)) {
			StopAllCoroutines ();
			if (seeds.transform.childCount >= 10 || cut) {
				Destroy (this.gameObject);
				return;
			}

			GameObject seed;
			Vector3 pos = new Vector3(Random.Range(seedPosMin.x, seedPosMax.x), Random.Range(seedPosMin.y, seedPosMax.y), seedPosMax.z);

			seed = Instantiate (seedPrefab, pos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
			seed.transform.eulerAngles = new Vector3 (0f, 90f, 0f);
			seed.transform.SetParent (seeds.transform);

			Destroy (this.gameObject);

		}
	}
}
