using System;
using UnityEngine;
[CreateAssetMenu(menuName = "SO Event/Void")]
public class VoidEvent : ScriptableObject
{
	public event Action OnEventRaised;

	public void RaiseEvent()
	{
		OnEventRaised?.Invoke();
	}
}
