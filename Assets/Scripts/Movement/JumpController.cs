using UnityEngine;

namespace Onion2D.Movement
{
	public class JumpController : MonoBehaviour
	{
		[SerializeField] Rigidbody2D body;
		[SerializeField] float jumpForce;
		[SerializeField] PlayerAnimationController playerAnimationController;
		public void PerformeJump()
		{
			body.AddForce(Vector2.up * jumpForce * Mathf.Sign(body.gravityScale), ForceMode2D.Impulse);
			playerAnimationController.PlayJumpAnimation();
		}

	}
}
