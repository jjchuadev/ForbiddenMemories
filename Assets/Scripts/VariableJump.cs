using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableJump : MonoBehaviour {

	public float fallMultiplier = 1.75f;
	public float lowJumpMultiplier = 99.25f;

	Rigidbody2D rb;

	void Awake()
	{
		rb=GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y *
			 fallMultiplier * Time.deltaTime;
		}
		/*else
		if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y *
			lowJumpMultiplier * Time.deltaTime;
		}*/
	}
}