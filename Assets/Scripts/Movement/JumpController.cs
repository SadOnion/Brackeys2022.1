using UnityEngine;

namespace Onion2D.Movement
{
	public class JumpController : MonoBehaviour
	{
		[SerializeField] Rigidbody2D body;
		[SerializeField] float jumpForce;
		[SerializeField] PlayerAnimationController playerAnimationController;
		[SerializeField] AudioClip jump;
		[SerializeField] FloatVariable volume;
		public void PerformeJump()
		{
			body.AddForce(Vector2.up * jumpForce * Mathf.Sign(body.gravityScale), ForceMode2D.Impulse);
			playerAnimationController.PlayJumpAnimation();
			LeanAudio.play(jump, volume.RuntimeValue);
		}

	}
}
