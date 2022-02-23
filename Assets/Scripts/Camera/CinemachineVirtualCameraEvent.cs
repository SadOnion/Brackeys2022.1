using System;
using UnityEngine;
using Cinemachine;
[CreateAssetMenu(menuName = "SO " +
	"Event/CinemachineVirtualCamera")]
public class CinemachineVirtualCameraEvent : ScriptableObject
{
	public event Action<CinemachineVirtualCamera> OnEventRaised;

	public void RaiseEvent(CinemachineVirtualCamera camera)
	{
		OnEventRaised?.Invoke(camera);
	}
}