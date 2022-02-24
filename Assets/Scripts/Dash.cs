using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onion2D.Movement;

[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    [SerializeField] LayerMask dashRefreshLayers;
    [SerializeField] Vector2 spawnPos = Vector2.zero;
    [SerializeField] AccelerationMovement accelerationMovement;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] DashGhost dashGhostPrefab;
/*    [SerializeField] AnimationCurve distanceCurve;*/
    [SerializeField] Player player;
    [SerializeField] float flightTime = 2f;
    /*[SerializeField] float baseDistance = 5f;
    [Range(0f, 0.1f)]
    [SerializeField] float performerVelocityImportance = 1f;*/

    new Rigidbody2D rigidbody2D;

    DashGhost dashGhost;

    Vector2 direction = Vector2.right;
    Vector2 sidewaysDirection = Vector2.right;

    bool ready = true;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player.onKilled.AddListener(() =>
        {
            if(dashGhost != null)
            {
                dashGhost.Destroy();
                dashGhost = null;
            }
        });
    }

    private void Update()
    {
        if (dashGhost == null && groundCheck.IsGroundedWith(dashRefreshLayers))
            ready = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + (Vector3)spawnPos, 0.05f);
    }


    public void UpdateDashDirection(Vector2 direction)
    {
        this.direction = direction;
        if (direction.x != 0)
            sidewaysDirection = new Vector2(direction.x, 0).normalized;
    }

    

    public void Perform()
    {
        if(dashGhost == null)
        {
            if (!ready)
                return;

            ready = false;
            Vector2 dashDirection = direction != Vector2.zero ? direction : sidewaysDirection;

            dashGhost = Instantiate(dashGhostPrefab, transform.position + (Vector3)spawnPos, Quaternion.identity)
                //.Initialize(rigidbody2D.velocity, performerVelocityImportance, baseDistance, flightTime, distanceCurve, dashDirection);
                .Initialize(dashDirection * accelerationMovement.MaxSpeed, flightTime);
            dashGhost.onDestroyed.AddListener(() => dashGhost = null);
        }
        else
        {
            rigidbody2D.position = dashGhost.transform.position;
            //rigidbody2D.velocity = dashGhost.Velocity;  
            dashGhost.Destroy();
            dashGhost = null;
        }
    }  
}
