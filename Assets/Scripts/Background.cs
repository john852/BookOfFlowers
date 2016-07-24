using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public FlowerBookSystem fbs;
	private SpriteRenderer sr; 
	private Color skyColor;

	// Use this for initialization
	public void Start () {
		fbs = this.gameObject.GetComponent<FlowerBookSystem> ();
		sr = this.GetComponent<SpriteRenderer> ();
		skyColor = sr.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (fbs == null) {
			fbs = this.gameObject.GetComponent<FlowerBookSystem> ();
		}
		float time = fbs.GetTime ();
		float limit = 12f * 60f;
		float green, blue;
		if (time < limit) {
			green = Mathf.Lerp (180f, 40f, (time / limit)) / 255f;
			blue = Mathf.Lerp (255f, 56f, (time / limit)) / 255f;
		} else {
			time -= limit;
			green = Mathf.Lerp (40f, 180f, (time / limit)) / 255f;
			blue = Mathf.Lerp (56f, 255f, (time / limit)) / 255f;
		}

		skyColor = new Color (skyColor.r, green, blue);
		sr.color = skyColor;
	}
}
