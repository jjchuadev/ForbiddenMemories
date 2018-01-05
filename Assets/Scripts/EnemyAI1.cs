using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI1 : MonoBehaviour {

	//Sprite Renderer
	SpriteRenderer sr;
	private Animator anim;

	[SerializeField]
	public int health = 2; 

	//what to chase = player
	public Transform target; 

	//how many times per second we will update our path
	public float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;

	//the calculated path
	public Path path;

	//The AI speed per second
	public float speed = 300f;
	public ForceMode2D fmode;

	[HideInInspector]
	public bool pathIsEnded = false;

	//the max distance from AI to a waypoint for it to continue to the next waypoint
	public float nextWayPointDistance = 3;

	//the waypoint we are currently moving towards
	private int currentWayPoint = 0;

	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();

		if (target == null) {
			//Debug.LogError ("No player found!");
			return;
		}

		//Start a new path to the target position, return the result th the OnPathComplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}

	IEnumerator UpdatePath () {
		if (target == null) {
			//Insert a player search here
			yield return false;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);
		yield return new WaitForSeconds (1f / updateRate);

		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete (Path p) {
		//Debug.Log ("We got a path. Did it have an error? " + p.error);
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
		}
	}

	void FixedUpdate () {
		if (target == null) {
			//Insert a player search here
			return;
		}

		//TODO: Always look at player?

		if (path == null) {
			return;

		}
		if (currentWayPoint >= path.vectorPath.Count) {
			if (pathIsEnded)
				return;

			//Debug.Log ("End of pathe reached.");
			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;

		//Direction to the next waypoint
		Vector3 dir = ( path.vectorPath[currentWayPoint] - transform.position ).normalized;

		//flip the direction that the sprite is facing
		//		transform.LookAt(target);
		Vector3 targetPosition = (transform.position - target.transform.position);
		if (targetPosition.x < 0) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}

		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		rb.AddForce (dir, fmode);
		//		rb.AddForce (Vector3.left * speed * Time.fixedDeltaTime, fmode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWayPoint]);

		if (dist < nextWayPointDistance) {
			currentWayPoint++;
			return;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name.Contains("Bullet")) {
			this.health -= 1; 
			SoundManagerScript.PlaySound ("enemyHitByLaser"); //jc
		}

		if (health <= 0) {
			anim.SetBool("dead", true);
			Destroy (this.gameObject); 
		}
	}
}
