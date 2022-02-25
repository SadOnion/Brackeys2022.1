using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpringboard : Springboard
{
	[SerializeField] float powerMultiplier = 1f;
	[SerializeField] float baseForce = 1f;
	protected override void OnBounce(Vector3 springBoardNormal, Rigidbody2D rigidbody, Vector2 velocityOnContact)
	{
		float bouncePower = Vector3.Project(-velocityOnContact, springBoardNormal).magnitude * powerMultiplier + baseForce;
		Vector3 bounceForce = springBoardNormal * bouncePower;
		rigidbody.AddForce(bounceForce, ForceMode2D.Impulse);
	}
}
