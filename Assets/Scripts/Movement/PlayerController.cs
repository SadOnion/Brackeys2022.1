using Onion2D.Movement;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : InputBehaviour
{
	[SerializeField] Rigidbody2D body;
	[SerializeField] AccelerationMovement accelerationMovement;
	[SerializeField] JumpController jumpController;
	[SerializeField] GroundCheck groundCheck;
	[SerializeField] Interactor interactor;
	[SerializeField] [Range(0, 1)] float jumpCut;
	[SerializeField] Dash dash;

	private void Update()
	{
		float input = controls.Player.Movement.ReadValue<float>();
		accelerationMovement.Move(input);
	}
	protected override void SubscribeInputEvents()
	{
		controls.Player.Jump.performed += Jump;
		controls.Player.Jump.canceled += JumpCanceled;
		controls.Player.Interact.performed += Interact;
		controls.Player.Dash.performed += Dash;
	}


	protected override void UnsubscribeInputEvents()
	{
		controls.Player.Jump.performed -= Jump;
		controls.Player.Jump.canceled -= JumpCanceled;
		controls.Player.Interact.performed -= Interact;
		controls.Player.Dash.performed -= Dash;
	}
	private void JumpCanceled(InputAction.CallbackContext obj)
	{
		if (body.velocity.y > 0)
		{
			body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpCut);
		}
	}

	private void Jump(InputAction.CallbackContext obj)
	{
		if (groundCheck.IsGrounded)
		{
			jumpController.PerformeJump();
		}
	}

	private void Interact(InputAction.CallbackContext obj) => interactor.TryInteract();

	private void Dash(InputAction.CallbackContext obj)
    {
		Vector2 dashDirection = controls.Player.DashDirection.ReadValue<Vector2>();
		dash.Perform(dashDirection);
	}
}
