using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class DashGhost : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    new Rigidbody2D rigidbody2D;

    Vector2 initialPosition;
    Vector2 targetPosition;

    float flightTime;
    float currentTime = 0f;
    AnimationCurve distanceCurve;

    public UnityEvent onDestroyed = new UnityEvent();

    Vector2 prevPos;

    public Vector2 Velocity => (rigidbody2D.position - prevPos) / Time.fixedDeltaTime;

    public DashGhost Initialize(Vector2 performerVelocity, float performerVelocityImportance, float baseDistance, float flightTime, AnimationCurve distanceCurve, Vector2 direction)
    {
        this.flightTime = flightTime;
        this.distanceCurve = distanceCurve;

        initialPosition = transform.position;
        Vector2 velocity = Vector3.Project(performerVelocity, direction);
        float modifiedDistance = baseDistance + baseDistance * velocity.magnitude * Mathf.Sign(Vector2.Dot(velocity, direction)) * performerVelocityImportance;
        targetPosition = initialPosition + direction.normalized * modifiedDistance;

        prevPos = initialPosition;
        
        return this;
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime > flightTime)
            Destroy(gameObject);
        else
        {
            Vector2 newPosition = Vector2.Lerp(initialPosition, targetPosition, distanceCurve.Evaluate(currentTime / flightTime));
            rigidbody2D.MovePosition(newPosition);
            prevPos = rigidbody2D.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer + "  " + groundLayer.value);

        if (groundLayer == (groundLayer | (1 << collision.gameObject.layer)))
        {
            Destroy();
            onDestroyed.Invoke();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
