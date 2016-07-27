using UnityEngine;
using System.Collections;

public class Sunflower: Flower {

	// Use this for initialization
	override public void Start () {
		minGrowthLength = 12f * 60f* growthFactor;
		maxGrowthLength = 24f * 60f* growthFactor;
		base.Start ();
		seedPrefab = Resources.Load("Seeds/SunFlowerSeed") as GameObject;
	}
	
	// Update is called once per frame
	override public void Update () {
		base.Update ();

		if (!cut) {
			if (watering) {
				foreach (Transform lev in leaves) {
					lev.GetComponent<Leave> ().leavTime += wateringFactor;
				}
			}

			if (!flowerbody.GetComponent<FlowerBody> ().active && !flowerbody.GetComponent<FlowerBody> ().decay && time >= ((1f / 4f) * growthLength)) {
				SetFlowerBodyActive ();
			}

			if (time >= leaveTimer && numOfLeaves > 0) {
				numOfLeaves--;
				leaveTimer += originalLeaveTimer;
				float yPos = Mathf.Lerp (0.2f, leaveYHeight, time / ((3f / 4f) * growthLength));

				if (leavePrefab.name == "LeftLeave") {
					leavePrefab = Resources.Load ("RightLeave", typeof(GameObject)) as GameObject;
					leavePrefab.name = "RightLeave";
				} else {
					leavePrefab = Resources.Load ("LeftLeave", typeof(GameObject)) as GameObject;
					leavePrefab.name = "LeftLeave";
				}

				GameObject leave; 
				leave = Instantiate (leavePrefab, new Vector3 (0.012f, yPos, 0f), Quaternion.identity) as GameObject;
				leave.transform.SetParent (leaves.transform, false);

				leave.GetComponent<Leave> ().leaveGrowthLength = ((3f / 4f) * growthLength) / 2f;
				leave.GetComponent<Leave> ().stopTime = ((3f / 4f) * growthLength) - time;
			}
		}

		if (time >= (growthLength * 1.5f)) {
			StopAllCoroutines ();

			if (seeds.transform.childCount >= 10 || cut) {
				Destroy (this.gameObject);
				return;
			}

			GameObject seed;
			Vector3 pos = new Vector3(Random.Range(seedPosMin.x, seedPosMax.x), Random.Range(seedPosMin.y, seedPosMax.y), seedPosMax.z);
			if (Random.Range (0, 2) > 0) {
				Debug.Log ("make two");
				seed = Instantiate (seedPrefab, pos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
				seed.transform.SetParent (seeds.transform);
				pos = new Vector3(Random.Range(seedPosMin.x, seedPosMax.x), Random.Range(seedPosMin.y, seedPosMax.y), seedPosMax.z);
				seed = Instantiate (seedPrefab, pos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
				seed.transform.SetParent (seeds.transform);
			} else {
				seed = Instantiate (seedPrefab, pos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
				seed.transform.eulerAngles = new Vector3 (0f, 90f, 0f);
				seed.transform.SetParent (seeds.transform);
			}


			Destroy (this.gameObject);

		}
	}
}
