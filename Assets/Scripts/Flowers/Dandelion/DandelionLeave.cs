using UnityEngine;
using System.Collections;

public class DandelionLeave : MonoBehaviour {

	protected bool decay = false;
	protected float leaveTime; 
	protected float rottenTime = 5f; 
	protected SpriteRenderer srLeave;

	public void StartDecay() {
		leaveTime = Time.deltaTime;
		this.GetComponent<Rigidbody> ().isKinematic = false;
		srLeave = this.GetComponent<SpriteRenderer> ();
		decay = true;
	}
	
	// Update is called once per frame
	public void Update () {
		if (decay) {
			leaveTime += Time.deltaTime;

			float blue, red, green, a;
			red = Mathf.Lerp (255f, 39f, (leaveTime / rottenTime)) / 255f;
			green = Mathf.Lerp (255f, 26f, (leaveTime / rottenTime)) / 255f;
			blue = Mathf.Lerp (255f, 26f, (leaveTime / rottenTime)) / 255f;
			a = Mathf.Lerp (255f, 50f, (leaveTime / rottenTime)) / 255f;

			srLeave.color = new Color (red, green, blue, a);

			if (a <= 51f/255f)
				Destroy (this.gameObject);

		}
	
	}
}
