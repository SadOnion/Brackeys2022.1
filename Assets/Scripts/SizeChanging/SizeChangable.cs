using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SizeChangable : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    SpriteRenderer spriteRenderer;
    Vector3 originalScale;
    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    Vector3 TopOfObject() => spriteRenderer.bounds.center + Vector3.up * spriteRenderer.bounds.extents.y;
    Vector3 BottomOfObject() => spriteRenderer.bounds.center - Vector3.up * spriteRenderer.bounds.extents.y;

    public void ModifySize(float multiplier)
    {
        ModifySize(Vector2.one * multiplier);
    }

    public void ModifySize(Vector2 multiplier)
    {
        RaycastHit2D hit = Physics2D.Raycast(TopOfObject(), Vector2.up, 10, groundLayer.value);
        float ceilingHeight = (hit.collider != null ? hit.point.y : float.PositiveInfinity);

        float initialBottomHeight = BottomOfObject().y;
        transform.localScale = Vector3.Scale(originalScale, multiplier);
        float finalBottomHeight = BottomOfObject().y;
        
        float heightCorrection = (initialBottomHeight - finalBottomHeight);
        {
            Vector3 correctedPosition = new Vector3(
                transform.position.x,
                transform.position.y + heightCorrection,
                transform.position.z
            );
            transform.position = correctedPosition;
            rigidbody2D.position = correctedPosition;
        }

  
        float distanceToCeiling = ceilingHeight - TopOfObject().y;
        if(distanceToCeiling < 0)
        {
            Vector3 correctedPosition = new Vector3(
                transform.position.x,
                transform.position.y + distanceToCeiling,
                transform.position.z
            );
            transform.position = correctedPosition;
            rigidbody2D.position = correctedPosition;
        }

    }

}
