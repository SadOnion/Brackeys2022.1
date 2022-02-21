using UnityEngine;
using UnityEngine.InputSystem;

namespace Onion2D.Movement
{
	public class JumpController : MonoBehaviour
	{
		[SerializeField] Rigidbody2D body;
		[SerializeField] float jumpForce;
		public void PerformeJump()
		{
			body.AddForce(Vector2.up * jumpForce * Mathf.Sign(body.gravityScale), ForceMode2D.Impulse);
		}

	}
}
