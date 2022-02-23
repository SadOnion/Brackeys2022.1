using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] Animator animator;
	public void PlayJumpAnimation() => animator.SetTrigger("Jump");
}
