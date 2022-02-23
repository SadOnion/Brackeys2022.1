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
	[SerializeField] float coyoteTime = .15f;
	[SerializeField] Dash dash;

	float lastTimeOnGround;
	float lastJumpInput;
	private void Update()
	{
		float input = controls.Player.Movement.ReadValue<float>();
		accelerationMovement.Move(input);
		UpdateLastTimeOnGround();
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
			CutVelocityY();
		}
	}
	private void CutVelocityY() => body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpCut);
	private void Jump(InputAction.CallbackContext obj)
	{

		if (groundCheck.IsGrounded || CanJumpWithoutGround())
		{
			jumpController.PerformeJump();
		}
		else
		{
			lastJumpInput = Time.time;
		}
	}

	private void Interact(InputAction.CallbackContext obj) => interactor.TryInteract();
	private bool CanJumpWithoutGround() => Time.time - lastTimeOnGround < coyoteTime;
	private void UpdateLastTimeOnGround()
	{
		if (groundCheck.IsGrounded)
		{
			lastTimeOnGround = Time.time;
			JumpBuffer();
		}
	}
	private void JumpBuffer()
	{
		if (Time.time - lastJumpInput <= coyoteTime)
		{
			lastJumpInput = 0;
			jumpController.PerformeJump();
			if (!controls.Player.Jump.IsPressed())
			{
				CutVelocityY();
			}
		}
	}
	private void Dash(InputAction.CallbackContext obj)
	{
		Vector2 dashDirection = controls.Player.DashDirection.ReadValue<Vector2>();
		dash.Perform(dashDirection);
	}
}
