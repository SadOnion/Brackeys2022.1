using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onion2D.Movement;

public class DirectionalSpringboard : Springboard
{
    [SerializeField] float power = 10f;
    [Tooltip("Describes for how long bounce restricts player's control")]
    [SerializeField] float controlRestorationTime = 1f;
    [Tooltip("Describes how much control player has during bounce at given time")]
    [SerializeField] AnimationCurve controlRestorationCurve;

    protected override void OnBounce(Vector3 springBoardNormal, Rigidbody2D rigidbody, Vector2 velocityOnContact)
    {
        rigidbody.velocity = Vector2.zero;
        float bouncePower = power;
        Vector3 bounceForce = springBoardNormal * bouncePower;
        rigidbody.AddForce(bounceForce, ForceMode2D.Impulse);
        
        AccelerationMovement accelerationMovement = rigidbody.GetComponent<AccelerationMovement>();
        if (accelerationMovement != null)
        {
            accelerationMovement.Control = 0f;
            gameObject.LeanValue(0f, 1f, controlRestorationTime)
                .setOnUpdate((float progress) => accelerationMovement.Control = controlRestorationCurve.Evaluate(progress))
                .setOnComplete(() => accelerationMovement.Control = 1f);
        }
    }
}
