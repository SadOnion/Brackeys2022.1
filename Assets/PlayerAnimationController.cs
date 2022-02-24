using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] Animator animator;
	public void PlayJumpAnimation() => animator.SetTrigger("Jump");
	public void PlayBounceAnimation() => animator.SetTrigger("Jump");
	public void PlayRespawnAnimation() => animator.SetTrigger("Respawn");
}
