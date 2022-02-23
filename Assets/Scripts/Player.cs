using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Onion2D.Movement;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Tooltip("If spawn point is not specified initial player position becomes spawn point")]
    [SerializeField] Transform spawnPoint;
    Vector3 initialPlayerPos;
    [SerializeField] ParticleSystem deathParticlesPrefab;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] ParticleSystem landingParticles;

    public UnityEvent onKilled = new UnityEvent();

    Checkpoint currentCheckpoint;

    new Rigidbody2D rigidbody2D;

    bool lastFrameGrounded = true;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPlayerPos = transform.position;
    }

    private void Update()
    {
        bool grounded = groundCheck.IsGrounded;
        if (grounded && !lastFrameGrounded)
            landingParticles.Play();
        lastFrameGrounded = grounded;
    }

    public void Hit()
    {
        onKilled.Invoke();
        ParticleSystem deathParticles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        Respawn();
    }

    public void Respawn()
    {
        lastFrameGrounded = true;
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
