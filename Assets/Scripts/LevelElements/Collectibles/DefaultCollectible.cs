using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public abstract class DefaultCollectible : MonoBehaviour, Collectible
{
    public abstract void Collect();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
