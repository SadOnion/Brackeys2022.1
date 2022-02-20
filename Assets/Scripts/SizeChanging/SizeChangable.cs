using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SizeChangable : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    Vector3 TopOfObject() => spriteRenderer.bounds.center + Vector3.up * spriteRenderer.bounds.extents.y;
    Vector3 BottomOfObject() => spriteRenderer.bounds.center - Vector3.up * spriteRenderer.bounds.extents.y;

    public void ModifySize(float multiplier)
    {
        ModifySize(Vector2.one * multiplier);
    }

    public void ModifySize(Vector2 multiplier)
    {
        RaycastHit2D hit = Physics2D.Raycast(TopOfObject(), Vector2.up);
        float ceilingHeight = (hit.collider != null ? hit.collider.ClosestPoint(TopOfObject()).y : float.PositiveInfinity);

        float initialBottomHeight = BottomOfObject().y;
        transform.localScale = Vector3.Scale(originalScale, multiplier);
        float finalBottomHeight = BottomOfObject().y;
        
        float heightCorrection = initialBottomHeight - finalBottomHeight;
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + heightCorrection,
            transform.position.z
        );

  
        float distanceToCeiling = ceilingHeight - TopOfObject().y;
        if(distanceToCeiling < 0)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + distanceToCeiling,
                transform.position.z
            );
        }

    }

}
