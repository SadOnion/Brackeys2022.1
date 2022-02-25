using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityDisabler : MonoBehaviour
{
	[SerializeField] GravitySystem gravitySystem;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		gravitySystem.SwapGravity(GravitySystem.Direction.Normal);
	}
}
