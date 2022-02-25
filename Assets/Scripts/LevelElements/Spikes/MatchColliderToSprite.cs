using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class MatchColliderToSprite : MonoBehaviour
{
    [SerializeField] float yPadding = 0f;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        boxCollider.size = spriteRenderer.size - new Vector2(0, yPadding);
        boxCollider.offset = Vector2.zero;
    }
}
