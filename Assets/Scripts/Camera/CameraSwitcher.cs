using Cinemachine;
using UnityEngine;
public class CameraSwitcher : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera activeCamera;
	[SerializeField] CinemachineVirtualCameraEvent OnCameraSwitch;

	private void OnEnable()
	{
		OnCameraSwitch.OnEventRaised += SwitchCamera;
	}
	private void OnDisable()
	{
		OnCameraSwitch.OnEventRaised -= SwitchCamera;
	}
	private void SwitchCamera(CinemachineVirtualCamera obj)
	{
		activeCamera.Priority = 0;
		activeCamera = obj;
		activeCamera.Priority = 10;
	}

}
