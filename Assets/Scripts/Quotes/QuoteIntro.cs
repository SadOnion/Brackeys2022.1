using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuoteIntro : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textField;

    [Header("Settings")]
    [SerializeField] float bgFadeInOutTime = 1f;
    [SerializeField] LeanTweenType fadeInType;
    [SerializeField] LeanTweenType fadeOutType;
    [SerializeField] float textDisplayDelay = 0f;
    [SerializeField] float textFadeInOutTime = 1f;

    public UnityEvent onFadeInFinished = new UnityEvent();
    public UnityEvent onFadeOutStarted = new UnityEvent();
    public UnityEvent onFinished = new UnityEvent();

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void Activate(QuoteSO quote)
    {
        textField.text = quote.Text;
        textField.alpha = 0f;

        LTSeq sequence = LeanTween.sequence();

        sequence.append(FadeInBackground());
        sequence.append(() => onFadeInFinished.Invoke());
        sequence.append(textDisplayDelay);
        sequence.append(FadeInText());
        sequence.append(quote.DisplayTime);
        sequence.append(FadeOutText());
        sequence.append(() => onFadeOutStarted.Invoke());
        sequence.append(FadeOutBackground());
        sequence.append(() => onFinished.Invoke());
    }

    LTDescr FadeInText()
    {
        return gameObject.LeanValue(0f, 1f, textFadeInOutTime)
            .setOnUpdate((float newAlpha) => textField.alpha = newAlpha)
            .setEase(fadeInType);
    }

    LTDescr FadeOutText()
    {
        return gameObject.LeanValue(1f, 0f, textFadeInOutTime)
            .setOnUpdate((float newAlpha) => textField.alpha = newAlpha)
            .setEase(fadeOutType);
    }

    LTDescr FadeInBackground()
    {
        return canvasGroup.LeanAlpha(1f, bgFadeInOutTime).setEase(fadeInType);
    }

    LTDescr FadeOutBackground()
    {
        return canvasGroup.LeanAlpha(0f, bgFadeInOutTime).setEase(fadeOutType);
    }
}
