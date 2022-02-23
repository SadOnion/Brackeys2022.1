using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Tooltip("If spawn point is not specified initial player position becomes spawn point")]
    [SerializeField] Transform spawnPoint;
    Vector3 initialPlayerPos;

    public UnityEvent onKilled = new UnityEvent();

    Checkpoint currentCheckpoint;

    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPlayerPos = transform.position;
    }

    public void Hit()
    {
        onKilled.Invoke();
        Respawn();
    }

    public void Respawn()
    {
        Vector2 respawnPosition;

        if (currentCheckpoint != null)
        {
            respawnPosition = currentCheckpoint.transform.position;
        }
        else
        {
            if (spawnPoint != null)
                respawnPosition = spawnPoint.position;
            else
                respawnPosition = initialPlayerPos;
        }

        rigidbody2D.position = respawnPosition;
        rigidbody2D.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
        if (checkpoint != null)
            currentCheckpoint = checkpoint;
    }
}
