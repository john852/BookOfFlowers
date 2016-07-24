using UnityEngine;
using System.Collections;

public class FlowerBody : MonoBehaviour {

	public float time;
	public float growthLength = 10f;
	protected float growthSize; 
	protected SpriteRenderer sr;
	public Transform leaveSprout;
	protected Flower mainFlower;

	public bool active = false;
	public bool decay = false;
	protected float decayTimer = 0f;
	protected SpriteRenderer srStem; 
	protected bool colliderEnabled = false;
	protected bool fallen = false;

	// Use this for initialization
	virtual public void Start () {
		time = Time.deltaTime;
		growthSize  = Random.Range (1f, 1.2f);
		this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
		sr = this.transform.GetChild (0).GetComponent<SpriteRenderer> ();
		srStem = this.transform.parent.GetChild (0).GetComponent<SpriteRenderer> ();
	}

	public void SetMainFlower(Flower f) {
		mainFlower = f;
		leaveSprout = f.stem.GetChild(0);
	}

	public void PutImagesInFront() {
		this.transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 3;
		Transform pedals = this.transform.GetChild (1);
		foreach (Transform child in pedals) {
			child.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 2;
		}
	}

	public void StartDecay() {
		decay = true;
		decayTimer = Time.deltaTime;
	}
		
	public IEnumerator StartFalling() {
		yield return new WaitForSeconds (2f);
		this.GetComponent<Rigidbody> ().isKinematic = true;

	}

	// Update is called once per frame
	virtual public void Update () {
		if (time > growthLength && active) {
			active = false;
		}
	}
}
