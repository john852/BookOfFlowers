using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public FlowerBookSystem fbs;
	private SpriteRenderer sr; 
	private Color skyColor;

	public AudioClip day;
	public AudioClip night;
	private AudioClip toBecome;

	private AudioSource audioS;

	public bool dayBool = true;

	// Use this for initialization
	public void Start () {
		fbs = this.gameObject.GetComponent<FlowerBookSystem> ();
		sr = this.GetComponent<SpriteRenderer> ();
		skyColor = sr.color;

		audioS = this.GetComponent<AudioSource> ();

		day = Resources.Load ("dayTime") as AudioClip;
		night = Resources.Load ("nightTime") as AudioClip;
	}

	private float soundTransitionTime = 0.1f;
	private float transition = 0.01f;
	IEnumerator TransitionSound() {
		yield return new WaitForSeconds (soundTransitionTime);

		audioS.volume -= transition;

		if (audioS.volume >= 0.9f) {
			transition = 0.01f;
		} else if (audioS.volume <= 0.1f) {
			transition = -transition;
			audioS.clip = toBecome;
			audioS.Play ();
			StartCoroutine (TransitionSound ());
		} else {
			StartCoroutine (TransitionSound ());
		}
	

	}

	// Update is called once per frame
	void Update () {
		if (fbs == null) {
			fbs = this.gameObject.GetComponent<FlowerBookSystem> ();
		}
		float time = fbs.GetTime ();
		float limit = 720f;
		float green, blue;
		if (time < limit) {
			if (!dayBool) {
				StopAllCoroutines ();
				dayBool = true;
				toBecome = day;
				Debug.Log ("Calling day time music");
				StartCoroutine (TransitionSound ());
				audioS.volume = 0.89f;

			}
			green = Mathf.Lerp (180f, 40f, (time / limit)) / 255f;
			blue = Mathf.Lerp (255f, 56f, (time / limit)) / 255f;
		} else {
			if (dayBool) {
				StopAllCoroutines ();
				dayBool = false;
				toBecome = night;
				Debug.Log ("Calling night time music");

				StartCoroutine (TransitionSound ());
				audioS.volume = 0.89f;
			}
			
			time -= limit;
			green = Mathf.Lerp (40f, 180f, (time / limit)) / 255f;
			blue = Mathf.Lerp (56f, 255f, (time / limit)) / 255f;
		}

		skyColor = new Color (skyColor.r, green, blue);
		sr.color = skyColor;
	}
}
