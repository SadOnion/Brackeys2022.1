using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
[CreateAssetMenu(menuName = "SO " +
	"Event/CinemachineVirtualCamera")]
public class CinemachineVirtualCameraSwitchSystem : ScriptableObject
{
	public event Action<CinemachineVirtualCamera> onCameraChanged;

	CinemachineVirtualCamera playerCamera;
	CinemachineVirtualCamera ghostCamera;

	/*	public void RaisePlayerCameraEvent(CinemachineVirtualCamera camera)
		{
			onCameraChanged?.Invoke(camera);
		}*/

	Coroutine delayedCoro;

	public void SetPlayerCamera(CinemachineVirtualCamera camera)
    {
		playerCamera = camera;
		UpdateCurrentCamera();
    }

	public void SetGhostCamera(CinemachineVirtualCamera camera)
    {
		if (delayedCoro != null)
			CameraSwitcher.instance.StopCoroutine(delayedCoro);
		delayedCoro = CameraSwitcher.instance.StartCoroutine(DelayedSetGhostCamera(camera));
	}

	IEnumerator DelayedSetGhostCamera(CinemachineVirtualCamera camera)
    {
		if (camera == null)
        {
			yield return new WaitForFixedUpdate();
			yield return new WaitForSeconds(0.3f);
		}
		ghostCamera = camera;
		UpdateCurrentCamera();
		delayedCoro = null;
	}

	void UpdateCurrentCamera()
    {
		if(playerCamera != null)
        {
			if (ghostCamera == null)
				onCameraChanged?.Invoke(playerCamera);
			else
				onCameraChanged?.Invoke(ghostCamera);
        }
    }
}