using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class SizeChangeableDetector : MonoBehaviour
{
    public UnityEvent<SizeChangable, bool> onDetected = new UnityEvent<SizeChangable, bool>();

    new Collider2D collider;

    SizeChangable detectedSizeChangeable;
    bool sizeChangeableOnRight;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedSizeChangeable = collision.gameObject.GetComponent<SizeChangable>();
        DetectPassingThrough(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DetectPassingThrough(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectPassingThrough(collision);
        detectedSizeChangeable = null;
    }

    void DetectPassingThrough(Collider2D collision)
    {
        bool objectOnRightSide = collider.bounds.center.x < collision.bounds.center.x;
        if (transform.lossyScale.x < 0)
            objectOnRightSide = !objectOnRightSide;

        if (sizeChangeableOnRight != objectOnRightSide)
        {
            sizeChangeableOnRight = objectOnRightSide;
            onDetected.Invoke(detectedSizeChangeable, !objectOnRightSide);
        }
    }

}
