using UnityEngine;
using System.Collections;

public class FlowerBookSystem : MonoBehaviour {

	private float time;
	private int hours;
	private GameObject clouds;
	private float cloudSpawnTimer;
		
	// Use this for initialization
	void Start () {
		clouds = GameObject.Find ("Clouds");
		time = Time.deltaTime;
		hours = 0;

		cloudSpawnTimer = Random.Range (60f, 120f);
		StartCoroutine (SpawnCloud ());
	}

	IEnumerator SpawnCloud() {
		yield return new WaitForSeconds(cloudSpawnTimer);
		cloudSpawnTimer = Random.Range (60f, 120f);

		float xPos = Random.Range (-3f, 0f);
		float zPos = Random.Range (-0.5f, -1f);


		GameObject cloud = Instantiate (Resources.Load ("cloudPrefab", typeof(GameObject)) as GameObject, 
			new Vector3(xPos, -9f, zPos), Quaternion.identity) as GameObject;
		cloud.transform.SetParent (clouds.transform);

		StartCoroutine (SpawnCloud ());
	}

	public float GetTime() {
		time += Time.deltaTime;
		hours = (int)(time / 60f);

		if (hours > 24) {
			time = Time.deltaTime;
			hours = (int)(time / 60f);
		}

//		Debug.Log (time);
//		Debug.Log ("hours: " + hours);

		return time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
