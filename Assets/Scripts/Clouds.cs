using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {
	public FlowerBookSystem fbs;
	private SpriteRenderer sr;

	private float xSpeed; 
	private float ySpeed;

	// Use this for initialization
	void Start () {
		fbs = GameObject.Find ("Background").GetComponent<FlowerBookSystem>();
		sr = this.GetComponent<SpriteRenderer> ();

		int ranImage = Random.Range (1, 5);

		switch (ranImage) {
		case 1:
			sr.sprite = Resources.Load ("clouds/cloud", typeof(Sprite)) as Sprite;
			break;
		case 2:
			sr.sprite = Resources.Load ("clouds/clouds2", typeof(Sprite)) as Sprite;
			break;
		case 3:
			sr.sprite = Resources.Load ("clouds/clouds3", typeof(Sprite)) as Sprite;
			break;
		case 4:
			sr.sprite = Resources.Load ("clouds/clouds4", typeof(Sprite)) as Sprite;
			break;
		}

		ranImage = Random.Range (1, 4);
		switch (ranImage) {
		case 1:
			sr.flipX = true;;
			break;
		case 2:
			sr.flipY = true;;
			break;
		case 3:
			sr.flipX = true;;
			sr.flipY = true;;
			break;
		}

		float time = fbs.GetTime ();
		float limit = 12f * 60f;
		float red, green, blue;
		if (time < limit) {
			red = Mathf.Lerp (255f, 128f, (time / limit)) / 255f;
			green = Mathf.Lerp (255f, 129f, (time / limit)) / 255f;
			blue = Mathf.Lerp (255f, 141f, (time / limit)) / 255f;
		} else {
			time -= limit;
			red = Mathf.Lerp (128f, 255f, (time / limit)) / 255f;
			green = Mathf.Lerp (129f, 255f, (time / limit)) / 255f;
			blue = Mathf.Lerp (141f, 255f, (time / limit)) / 255f;
		}
		sr.color = new Color (red, green, blue);

		xSpeed = Random.Range (0.0001f, .0005f);
		ySpeed = Random.Range (0.0005f, .001f);
	}
	
	// Update is called once per frame
	void Update () {
		float time = fbs.GetTime ();
		float limit = 12f * 60f;
		float red, green, blue;
		if (time < limit) {
			red = Mathf.Lerp (255f, 128f, (time / limit)) / 255f;
			green = Mathf.Lerp (255f, 129f, (time / limit)) / 255f;
			blue = Mathf.Lerp (255f, 141f, (time / limit)) / 255f;
		} else {
			time -= limit;
			red = Mathf.Lerp (128f, 255f, (time / limit)) / 255f;
			green = Mathf.Lerp (129f, 255f, (time / limit)) / 255f;
			blue = Mathf.Lerp (141f, 255f, (time / limit)) / 255f;
		}
		sr.color = new Color (red, green, blue);

		this.transform.Translate (xSpeed, ySpeed, 0);
		if (this.transform.localPosition.y >= 9.6f)
			Destroy (this.gameObject);
	}
}
