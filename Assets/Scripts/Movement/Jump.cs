using UnityEngine;
using UnityEngine.InputSystem;

namespace Onion2D.Movement
{
	public class Jump : InputBehaviour
	{
		[SerializeField] Rigidbody2D body;
		[SerializeField] float jumpForce;

		public void PerformeJump(InputAction.CallbackContext context)
		{
			body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		protected override void SubscribeInputEvents()
		{
			controls.Player.Jump.performed += PerformeJump;
		}

		protected override void UnsubscribeInputEvents()
		{
			controls.Player.Jump.performed -= PerformeJump;
		}
	}
}
