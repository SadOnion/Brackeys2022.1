using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Tooltip("If spawn point is not specified initial player position becomes spawn point")]
    [SerializeField] Transform spawnPoint;
    Vector3 initialPlayerPos;

    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPlayerPos = transform.position;
    }

    public void Hit()
    {
        Respawn();
    }

    public void Respawn()
    {
        if (spawnPoint != null)
            rigidbody2D.position = spawnPoint.position;
        else
            rigidbody2D.position = initialPlayerPos;
        rigidbody2D.velocity = Vector2.zero;
    }
}
