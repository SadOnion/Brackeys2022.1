using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangingGate : MonoBehaviour
{
    [SerializeField] Vector2 inScale = new Vector2(1, 1);
    [SerializeField] Vector2 outScale = new Vector2(2, 2);
    [SerializeField] SizeChangeableDetector sizeChangeableDetector;

    private void Start()
    {
        sizeChangeableDetector.onDetected.AddListener((SizeChangable sizeChangeable, bool fromRightSide) =>
        {
            if (fromRightSide)
                sizeChangeable.ModifySize(inScale);
            else
                sizeChangeable.ModifySize(outScale);
        });
    }
}
