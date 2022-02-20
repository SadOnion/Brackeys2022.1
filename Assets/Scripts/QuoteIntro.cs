using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuoteIntro : MonoBehaviour
{
    [SerializeField] float fadeInOutTime = 1f;
    [SerializeField] LeanTweenType fadeInType;
    [SerializeField] LeanTweenType fadeOutType;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textField;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void Activate(QuoteSO quote)
    {
        textField.text = quote.Text;

        LTSeq sequence = LeanTween.sequence();

        sequence.append(FadeIn());
        sequence.append(quote.DisplayTime);
        sequence.append(FadeOut());
    }

    LTDescr FadeIn()
    {
        return canvasGroup.LeanAlpha(1f, fadeInOutTime);
    }

    LTDescr FadeOut()
    {
        return canvasGroup.LeanAlpha(0f, fadeInOutTime);
    }
}
