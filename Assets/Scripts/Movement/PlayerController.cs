using Onion2D.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : InputBehaviour
{
	[SerializeField] AccelerationMovement accelerationMovement;
	[SerializeField] JumpController jumpController;
	[SerializeField] GroundCheck groundCheck;
	[SerializeField] Interactor interactor;

	private void Update()
	{
		float input = controls.Player.Movement.ReadValue<float>();
		accelerationMovement.Move(input);
	}
	protected override void SubscribeInputEvents()
	{
		controls.Player.Jump.performed += Jump;
		controls.Player.Interact.performed += Interact;
	}
	protected override void UnsubscribeInputEvents()
	{
		controls.Player.Jump.performed -= Jump;
		controls.Player.Interact.performed -= Interact;
	}

	private void Jump(InputAction.CallbackContext obj)
	{
		if (groundCheck.IsGrounded)
		{
			jumpController.PerformeJump();
		}
	}

	private void Interact(InputAction.CallbackContext obj) => interactor.TryInteract();

}
