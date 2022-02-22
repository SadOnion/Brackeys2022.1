using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    [SerializeField] DashGhost dashGhostPrefab;
    [SerializeField] AnimationCurve distanceCurve;
    [SerializeField] float flightTime = 2f;
    [SerializeField] float baseDistance = 5f;
    [Range(0f, 0.1f)]
    [SerializeField] float performerVelocityImportance = 1f;

    new Rigidbody2D rigidbody2D;

    DashGhost dashGhost;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Perform(Vector2 direction)
    {
        if(dashGhost == null)
        {
            if (direction == Vector2.zero)
                return;

            dashGhost = Instantiate(dashGhostPrefab, transform.position, Quaternion.identity)
                .Initialize(rigidbody2D.velocity, performerVelocityImportance, baseDistance, flightTime, distanceCurve, direction);
            dashGhost.onDestroyed.AddListener(() => dashGhost = null);
        }
        else
        {
            rigidbody2D.position = dashGhost.transform.position;
            rigidbody2D.velocity = dashGhost.Velocity;
            dashGhost.Destroy();
        }
    }  
}
