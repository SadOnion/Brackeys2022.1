using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityChanger : MonoBehaviour, Interactable
{
	[SerializeField] VoidEvent OnGravityChange;

	public void Interact()
	{
		OnGravityChange.RaiseEvent();
	}
}
