using UnityEngine;
using System.Collections;

public class Leave : Petals {

	public float leaveGrowthLength = 10f;
	public float stopTime;
	private float growthSize; 

	public float leavTime;
	private SpriteRenderer srLeave;

	// Use this for initialization
	void Start () {
		leavTime = Time.deltaTime;
		this.transform.localScale = new Vector3 (0f, 0f, 0f);
		growthSize  = Random.Range (.9f, 1.2f);
		this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 10f));
		srLeave = this.transform.GetChild (0).GetComponent<SpriteRenderer> ();

	}
		
	// Update is called once per frame
	override public void Update () {
		leavTime += Time.deltaTime;
		float scale = Mathf.Lerp (0f, growthSize, leavTime / leaveGrowthLength);
		this.transform.localScale = new Vector3 (scale, scale, 0f); 

		if (leavTime < stopTime) {
			this.transform.Translate (0f, 0.0001f, 0f);
		}

		if (fallen) {
			time += Time.deltaTime;

			float blue, red, green, a;
			red = Mathf.Lerp (255f, 39f, (time / rottenTime)) / 255f;
			green = Mathf.Lerp (255f, 26f, (time / rottenTime)) / 255f;
			blue = Mathf.Lerp (255f, 26f, (time / rottenTime)) / 255f;
			a = Mathf.Lerp (255f, 50f, (time / rottenTime)) / 255f;

			srLeave.color = new Color (red, green, blue, a);

			if (a <= 51f/255f)
				Destroy (this.gameObject);
		}
	}
}
