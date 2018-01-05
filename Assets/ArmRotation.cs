using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

	public Transform target;
	// Use this for initialization
	public int rotationOffset = 90;

	public static Quaternion armRotation;

	// Update is called once per frame
	void Update () {

		//position of target vs position of enemy
		Vector3 difference = target.position - transform.position;
		difference.Normalize (); //normalizing the vector. Means that all the sum of the vector will be equal to 1.

		//find the angle
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		armRotation = transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
	}
}
