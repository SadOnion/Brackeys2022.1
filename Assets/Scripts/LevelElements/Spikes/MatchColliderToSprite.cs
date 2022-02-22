using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class MatchColliderToSprite : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 inverseScale = new Vector2(1f / spriteRenderer.transform.localScale.x, 1f / spriteRenderer.transform.localScale.y);
        boxCollider.size = Vector2.Scale(spriteRenderer.bounds.size, inverseScale);
        boxCollider.offset = Vector2.zero;
    }
}
