using UnityEngine;
using System.Collections;

public class SunFlowerBody : FlowerBody {

	// Use this for initialization
	override public void Start () {
		base.Start ();
		this.transform.localScale = new Vector3 (0f, 0f, 0f);
		PutImagesInFront ();
	}
	
	// Update is called once per frame
	override public void Update () {
		float red, green, blue, a, a2;

		if (active) {
			time += Time.deltaTime;
			float scale = Mathf.Lerp (0f, growthSize, time / growthLength);
			this.transform.localScale = new Vector3 (scale, scale, 0f); 

			if (leaveSprout != null) {
				foreach (Transform child in leaveSprout) {
					child.localScale = new Vector3 (1 - scale, 1 - scale, 0f);
				}
			}

			red = Mathf.Lerp (180f, 100f, (time / growthLength)) / 255f;
			green = Mathf.Lerp (100f, 55f, (time / growthLength)) / 255f;

			sr.color = new Color (red, green, 0f);
		}

		if (decay) {
			decayTimer += Time.deltaTime;
			red = Mathf.Lerp (255f, 165f, (decayTimer / growthLength/2f)) / 255f;
			green = Mathf.Lerp (255f, 165f, (decayTimer / growthLength/2f)) / 255f;
			blue = Mathf.Lerp (255f, 165f, (decayTimer / growthLength/2f)) / 255f;
			a = Mathf.Lerp (255f, 20f, (decayTimer / growthLength/2f)) / 255f;

			srStem.color = new Color (red, green, blue, a);

			if (decayTimer > (growthLength / 2f)) {
				time += Time.deltaTime;
				a2 = Mathf.Lerp (255f, 20f, (time / growthLength/2f)) / 255f;

				if (!fallen) {
					fallen = true;
					this.GetComponent<Rigidbody> ().isKinematic = false;
					StartCoroutine (StartFalling ());
				}
				sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, a2);
			}
		}
		base.Update ();
	}
}
