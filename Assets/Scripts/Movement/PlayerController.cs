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
	}


	protected override void UnsubscribeInputEvents()
	{
		controls.Player.Jump.performed -= Jump;
		controls.Player.Jump.canceled -= JumpCanceled;
		controls.Player.Interact.performed -= Interact;
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
		lastJumpInput = Time.time;
		if (groundCheck.IsGrounded || CanJumpWithoutGround())
		{
			jumpController.PerformeJump();
		}
	}

	private void Interact(InputAction.CallbackContext obj) => interactor.TryInteract();
	private bool CanJumpWithoutGround() => Time.time - lastTimeOnGround < coyoteTime;
	private void UpdateLastTimeOnGround()
	{
		if (groundCheck.IsGrounded)
		{
			lastTimeOnGround = Time.time;
		}
	}
	private void JumpBuffer()
	{

	}
}
