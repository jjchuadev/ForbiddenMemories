using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {
	public Sprite laserOnSprite;
	public Sprite laserOffSprite;

	public BoxCollider2D laserCollider;


	[SerializeField]
	float timer; //1f
	[SerializeField]
	float delay; //3f

	private bool isLaserOn = false;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = laserOnSprite;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {

			//Change sprite to laser off
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == laserOnSprite) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = laserOffSprite;
				timer = delay;
				laserCollider.enabled = false;
				return;
			}

			//Change sprite to laser on
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == laserOffSprite) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = laserOnSprite;
				timer = delay;
				laserCollider.enabled = true;
				return;
			}

		}
	}
}
