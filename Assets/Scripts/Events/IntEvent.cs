using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO Event/Int")]
public class IntEvent : ScriptableObject
{
	public event Action<int> OnEventRaised;

	public void RaiseEvent(int arg)
	{
		OnEventRaised?.Invoke(arg);
	}
}
