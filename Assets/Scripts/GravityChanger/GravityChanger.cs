using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityChanger : MonoBehaviour, Interactable
{
    GravitySystem gravitySystem;

    private void Awake()
    {
        gravitySystem = FindObjectOfType<GravitySystem>();
    }

    public void Interact()
    {
        gravitySystem.SwapGravity();
    }
}
