using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCameraSwitchSystem cameraSwitchSystem;
	[SerializeField] Cinemachine.CinemachineVirtualCamera vCamera;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
        {
			cameraSwitchSystem.SetPlayerCamera(vCamera);
        }
		else
		{
			DashGhost ghost = collision.GetComponent<DashGhost>();
			if (ghost != null)
			{
				cameraSwitchSystem.SetGhostCamera(vCamera);
				ghost.onDestroyed.AddListener(() => cameraSwitchSystem.SetGhostCamera(null));
			}
		}
	}
}
