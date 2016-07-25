using UnityEngine;
using System.Collections;

public class FlowerBookSystem : MonoBehaviour {

	private float time;
	private int hours;
	private GameObject clouds;
	private float cloudSpawnTimer;

	private GameObject seeds;
	private GameObject seedPrefab;
	private GameObject dandelionPrefab;

	private float seedWaitSpawnTime = 10f;
	private float dandelionSpawnTime;

	protected Vector3 seedPosMax = new Vector3 (6.1f, -1.1f, -5f);
	protected Vector3 seedPosMin = new Vector3 (4.8f, -3.5f, -5f);
		
	// Use this for initialization
	void Start () {
		clouds = GameObject.Find ("Clouds");
		time = Time.deltaTime;
		hours = 0;
		seeds = GameObject.Find ("Seeds");

		cloudSpawnTimer = Random.Range (60f, 120f);
		seedWaitSpawnTime = 3f*60f;
		dandelionSpawnTime = 1.5f * seedWaitSpawnTime;
		dandelionPrefab = Resources.Load("Seeds/DandelionSeed") as GameObject;

			
		StartCoroutine (SpawnCloud ());
		StartCoroutine (SpawnSeed());
		StartCoroutine (SpawnDandelion());

	}

	IEnumerator SpawnDandelion() {
		yield return new WaitForSeconds(dandelionSpawnTime);

		GameObject seed;
		Vector3 pos = new Vector3(Random.Range(-6.5f, 3.7f), Random.Range(4.5f, 4.7f), -5f);

		seed = Instantiate (dandelionPrefab, pos , Quaternion.identity) as GameObject;
		seed.GetComponent<Collider> ().isTrigger = false;
		seed.GetComponent<Rigidbody> ().isKinematic = false;

		seed.transform.SetParent (seeds.transform);

		StartCoroutine (SpawnDandelion());
	}

	IEnumerator SpawnSeed() {
		if (seeds.transform.childCount <= 10) {
			int ran = Random.Range (0, 3);

			GameObject seed;
			Vector3 pos = new Vector3(Random.Range(seedPosMin.x, seedPosMax.x), Random.Range(seedPosMin.y, seedPosMax.y), seedPosMax.z);
			Quaternion rot = Quaternion.identity;
			switch (ran) {
			case 0:
				seedPrefab = Resources.Load ("Seeds/SunFlowerSeed") as GameObject;
				rot = Quaternion.Euler (0f, 90f, 0f);
				break;
			case 1:
				seedPrefab = Resources.Load("Seeds/RoseSeed") as GameObject;
				break;
			case 2:
				seedPrefab = Resources.Load("Seeds/OrchidSeed") as GameObject;
				break;
			}

			seed = Instantiate (seedPrefab, pos, rot) as GameObject;
			seed.transform.SetParent (seeds.transform);
		} 

		yield return new WaitForSeconds(seedWaitSpawnTime);

		StartCoroutine (SpawnSeed());
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
