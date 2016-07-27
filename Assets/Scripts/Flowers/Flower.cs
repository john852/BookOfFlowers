using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

	protected float time;
	public bool watering = false;
	protected int hours;
	public float growthLength;

	protected float minGrowthLength = 5f;
	protected float maxGrowthLength = 10f;
	protected float growthFactor = 1f;

	public Transform stem;
	protected Transform leaves;

	protected int numOfLeaves;

	protected float leaveTimer;
	protected float originalLeaveTimer;

	protected float leaveYHeight;
	protected GameObject leavePrefab;
	protected GameObject flowerbody;

	protected bool decaying = false;
	protected float decayTime;
	protected GameObject seeds;
	protected GameObject seedPrefab;

	protected Vector3 seedPosMax = new Vector3 (6.1f, -1.1f, -5f);
	protected Vector3 seedPosMin = new Vector3 (4.8f, -3.5f, -5f);


	// Use this for initialization
	virtual public void Start () {
		time = Time.deltaTime;
		growthLength = Random.Range (minGrowthLength, maxGrowthLength);

		float ramSize = Random.Range (.8f, 1.4f);
		numOfLeaves = Random.Range (4, 6);

		leaveTimer = ((3f/4f) * growthLength) / numOfLeaves;
		originalLeaveTimer = leaveTimer;


		this.transform.localScale = new Vector3 (ramSize, ramSize, 0f);
		leaveYHeight = ramSize * .8f;

		stem = this.transform.GetChild (1);
		stem.localScale = new Vector3 (0f, 0f, 0f ); 
		flowerbody = stem.GetChild (1).gameObject;
//		Debug.Log (flowerbody.name);
//		Debug.Log (flowerbody.activeSelf);

		leavePrefab = Resources.Load ("LeftLeave", typeof(GameObject)) as GameObject;
		leavePrefab.name = "LeftLeave";
		leaves = this.transform.GetChild (0);
		seeds = GameObject.Find ("Seeds");

		rotationSpeed = Random.Range(-0.3f, 0.3f);
		while (rotationSpeed >= -.1f && rotationSpeed <= .1f) {
			rotationSpeed = Random.Range(-0.3f, 0.3f);
		}
	}

	virtual public void SetFlowerBodyActive() {
		flowerbody.GetComponent<FlowerBody> ().active = true;
		flowerbody.GetComponent<FlowerBody> ().SetMainFlower (this);
		FlowerBody fb = flowerbody.GetComponent<FlowerBody> ();
		fb.growthLength = growthLength / 2f;

	}

	public bool cut = false;
	public float rotationSpeed;

	virtual public void Cut() {
		StartCoroutine (Decay ());
		growthLength /= 2f;
		time = growthLength * 1.8f;
			
//		if (time >= (growthLength * 2f))
//			time = growthLength * (1.5f);
		cut = true;
	}

	virtual public IEnumerator Decay() {
		decayTime = Random.Range (((1f/4f)*growthLength)/34f, ((1f/4f)*growthLength)/30f);
		yield return new WaitForSeconds (decayTime);

		int ran = Random.Range (0, 3);

		if (leaves.childCount > 0 && ran < 1) {
			leaves.GetChild (Random.Range(0, leaves.transform.childCount)).GetComponent<Leave> ().StartRotting ();
		}


		Transform petals = flowerbody.transform.GetChild (1);
		if (petals.childCount == 1) {
			petals.GetChild (0).GetComponent<Petals> ().StartRotting ();
		} else if (petals.childCount > 1) {
			petals.GetChild (Random.Range(0, petals.transform.childCount)).GetComponent<Petals> ().StartRotting ();
			StartCoroutine (Decay());
		}

	}
		
	protected float wateringFactor = 0.1f;
	// Update is called once per frame
	virtual public void Update () {

		if (watering) {
			time += wateringFactor;
			if (flowerbody.GetComponent<FlowerBody> ().active) {
				flowerbody.GetComponent<FlowerBody> ().time += wateringFactor;
			}
		}

		if (!cut) {
			time += Time.deltaTime;
			hours = (int)(time / 60f);

			float yScale = Mathf.Lerp (0f, 1f, time / ((3f / 4f) * growthLength));

			stem.localScale = new Vector3 (yScale, yScale, 0f); 


			if (!decaying && time > ((3f / 4f) * growthLength)) {
				decaying = true;
				StartCoroutine (Decay ());
				flowerbody.GetComponent<FlowerBody> ().StartDecay ();
			}
		} else {
			Vector3 rot = this.transform.eulerAngles; 

			if ((rot.z <= 80f && rot.z >= 0) || (rot.z >= 280f && rot.z <= 360)) {
				this.transform.Rotate (new Vector3 (0f, 0f, rotationSpeed));
				this.transform.GetChild (2).Rotate (new Vector3 (0f, 0f, -rotationSpeed));
			}

		}
	}
}
