using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onion2D.Movement;

[RequireComponent(typeof(Collider2D))]
public abstract class Springboard : MonoBehaviour
{
    [SerializeField] Animator animator;

    new Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if(otherRigidbody != null)
            Bounce(otherRigidbody, collision.relativeVelocity);
    }

    void Bounce(Rigidbody2D rigidbody, Vector2 velocityOnContact)
    {
        Vector3 springBoardNormal = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.up;
        OnBounce(springBoardNormal, rigidbody, velocityOnContact);
        if (animator != null)
            animator.SetTrigger("bounce");
        Player player = rigidbody.GetComponent<Player>();
        if (player != null)
            player.BounceAnimation();
    }

    protected abstract void OnBounce(Vector3 springBoardNormal, Rigidbody2D rigidbody, Vector2 velocityOnContact);
}
