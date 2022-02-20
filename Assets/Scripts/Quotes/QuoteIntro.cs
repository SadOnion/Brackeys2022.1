using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuoteIntro : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] QuoteSO quote;

    [Header("Dependencies")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textField;

    [Header("Settings")]
    [SerializeField] float bgFadeInOutTime = 1f;
    [SerializeField] LeanTweenType fadeInType;
    [SerializeField] LeanTweenType fadeOutType;
    [SerializeField] float textDisplayDelay = 0f;
    [SerializeField] float textFadeInOutTime = 1f;

    public UnityEvent onShowTextFinished = new UnityEvent();
    public UnityEvent onHideBackgroundFinished = new UnityEvent();
    public UnityEvent onShowBackgroundFinished = new UnityEvent();


    private void Awake()
    {
        textField.text = quote.Text;
        textField.alpha = 0f;
    }

    public void ShowText()
    {
        LTSeq sequence = LeanTween.sequence();

        sequence.append(FadeInText());
        sequence.append(quote.DisplayTime);
        sequence.append(FadeOutText());
        sequence.append(() => onShowTextFinished?.Invoke());
    }
    
    public void HideBackground()
    {
        LTSeq sequence = LeanTween.sequence();

        sequence.append(FadeOutBackground());
        sequence.append(() => onHideBackgroundFinished.Invoke());
    }

    public void ShowBackground()
    {
        LTSeq sequence = LeanTween.sequence();

        sequence.append(FadeInBackground());
        sequence.append(() => onShowBackgroundFinished.Invoke());
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
