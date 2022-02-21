using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DefaultGravitable : MonoBehaviour, Gravitable
{
    protected new Rigidbody2D rigidbody2D;

    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        FindObjectOfType<GravitySystem>().Subscribe(this);
    }

    public virtual void SwapGravity(Gravitable.Dir gravityDir)
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            -transform.localScale.y,
            transform.localScale.z
        );
        rigidbody2D.gravityScale *= -1;
    }
}
