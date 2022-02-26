using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverInflate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float ScaleMultiplier = 1.1f;
    [SerializeField] float ScalingSpeed = 1f;

    float scaleProgress = 0f;

    Vector3 originalScale;

    bool pointerInside = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (pointerInside)
            scaleProgress += Time.deltaTime * ScalingSpeed;
        else
            scaleProgress -= Time.deltaTime * ScalingSpeed;

        scaleProgress = Mathf.Clamp01(scaleProgress);

        transform.localScale = Vector3.Lerp(originalScale, originalScale * ScaleMultiplier, scaleProgress);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerInside = false;
    }


    
}
