using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class KondziuTest : MonoBehaviour
{
    [SerializeField] QuoteIntro quoteIntro;
    [SerializeField] QuoteSO testQuote;

    private void Start()
    {
        quoteIntro.onFadeInFinished.AddListener(() => Debug.Log("On fade in finished"));
        quoteIntro.onFadeOutStarted.AddListener(() => Debug.Log("On fade out started"));
        quoteIntro.onFinished.AddListener(() => Debug.Log("On finished"));
    }
    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard.jKey.wasPressedThisFrame)
            quoteIntro.Activate(testQuote);
    }
}
