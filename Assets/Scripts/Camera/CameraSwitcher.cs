using Cinemachine;
using UnityEngine;
public class CameraSwitcher : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera activeCamera;
	[SerializeField] CinemachineVirtualCameraSwitchSystem OnCameraSwitch;

	public static CameraSwitcher instance;

    private void Awake()
    {
		instance = this;
    }

    private void OnEnable()
	{
		OnCameraSwitch.onCameraChanged += SwitchCamera;
	}
	private void OnDisable()
	{
		OnCameraSwitch.onCameraChanged -= SwitchCamera;
	}
	private void SwitchCamera(CinemachineVirtualCamera obj)
	{
		activeCamera.Priority = 0;
		activeCamera = obj;
		activeCamera.Priority = 10;
	}

}
