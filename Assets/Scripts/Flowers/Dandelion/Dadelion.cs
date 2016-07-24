using UnityEngine;
using System.Collections;

public class Dadelion : Flower {

	private int leaveNum = 2;
	// Use this for initialization
	override public void Start () {
		minGrowthLength = 6f * 60f* growthFactor;
		maxGrowthLength = 12f * 60f* growthFactor;

		base.Start ();
		seedPrefab = Resources.Load("Seeds/DandelionSeed") as GameObject;
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

		if (!flowerbody.GetComponent<FlowerBody>().active && !flowerbody.GetComponent<FlowerBody>().decay && time >= ((1f / 5f) * growthLength) && !cut) {
			SetFlowerBodyActive ();
		}

		if (time >= (growthLength * 2f)) {
			StopAllCoroutines ();

			if (seeds.transform.childCount >= 10 || cut) {
				Destroy (this.gameObject);
				return;
			}

			GameObject seed;
			Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y+1, seedPosMax.z);
			Vector3 force = new Vector3(Random.Range(-.2f, .2f), Random.Range(.2f, .5f), 0f);
			if (Random.Range (0, 2) > 0) {
				Debug.Log ("make two");
				seed = Instantiate (seedPrefab, pos, Quaternion.identity) as GameObject;
				seed.GetComponent<Collider> ().isTrigger = false;
				seed.GetComponent<Rigidbody> ().isKinematic = false;
				seed.GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);
				seed.transform.SetParent (seeds.transform);

				force = new Vector3(Random.Range(-.1f, .1f), Random.Range(.3f, .4f), 0f);
				seed = Instantiate (seedPrefab, pos , Quaternion.identity) as GameObject;
				seed.GetComponent<Collider> ().isTrigger = false;
				seed.GetComponent<Rigidbody> ().isKinematic = false;
				seed.GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);

			} else {
				seed = Instantiate (seedPrefab, pos , Quaternion.identity) as GameObject;
				seed.GetComponent<Collider> ().isTrigger = false;
				seed.GetComponent<Rigidbody> ().isKinematic = false;
				seed.GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);

				seed.transform.SetParent (seeds.transform);
			}


			Destroy (this.gameObject);

		}
	}
}
