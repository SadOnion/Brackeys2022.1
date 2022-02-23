using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCameraEvent OnCameraSwitch;
	[SerializeField] Cinemachine.CinemachineVirtualCamera vCamera;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnCameraSwitch.RaiseEvent(vCamera);
	}
}
