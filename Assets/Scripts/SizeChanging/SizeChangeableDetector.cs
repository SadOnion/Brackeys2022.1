using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class SizeChangeableDetector : MonoBehaviour
{
    public UnityEvent<SizeChangable, bool> onDetected = new UnityEvent<SizeChangable, bool>();

    Collider2D detectorCollider;

    SizeChangable detectedSizeChangeable;

    enum Side { In, Out }
    Side? prevObjectPos;

    private void Awake()
    {
        detectorCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedSizeChangeable = collision.gameObject.GetComponent<SizeChangable>();
        if (detectedSizeChangeable == null)
            return;

        prevObjectPos = GetObjectPos(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (detectedSizeChangeable == null)
            return;

        if(PassedThrough(collision))
            onDetected.Invoke(detectedSizeChangeable, GetObjectPos(collision) == Side.In ? true : false);
        prevObjectPos = GetObjectPos(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (detectedSizeChangeable == null)
            return;

        if (PassedThrough(collision))
            onDetected.Invoke(detectedSizeChangeable, GetObjectPos(collision) == Side.In ? true : false);
        detectedSizeChangeable = null;
    }

    Side GetObjectPos(Collider2D collision)
    {
        Side side = (detectorCollider.bounds.center.x < collision.bounds.center.x) ? Side.Out : Side.In;
        if (transform.lossyScale.x < 0)
            side = OppositeSide(side);
        return side;
    }

    Side OppositeSide(Side side)
    {
        return side == Side.In ? Side.Out : Side.In;
    }

    bool PassedThrough(Collider2D collision)
    {
        return prevObjectPos.HasValue && GetObjectPos(collision) != prevObjectPos;
    }

}
