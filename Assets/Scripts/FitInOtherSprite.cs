using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FitInOtherSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer otherSpriteRenderer;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        float size = Mathf.Min(otherSpriteRenderer.size.x, otherSpriteRenderer.size.y);
        Vector3 otherScale = otherSpriteRenderer.transform.lossyScale;
        Vector3 invOtherScale = new Vector3(1f / otherScale.x, 1f / otherScale.y, 1f / otherScale.z);

        spriteRenderer.transform.localScale = invOtherScale;// Vector3.Scale(transform.parent.lossyScale, invOtherScale /*Vector3.Scale(invOtherScale, invOtherScale)*/);
        spriteRenderer.transform.localScale *= transform.parent.lossyScale.x;
    }
}
