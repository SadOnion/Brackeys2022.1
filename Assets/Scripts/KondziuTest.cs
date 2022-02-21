using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class KondziuTest : MonoBehaviour
{
    [SerializeField] QuoteIntro quoteIntro;
    [SerializeField] QuoteSO testQuote;
    [SerializeField] SizeChangable sizeChangable;
    [SerializeField] GravityChanger gravityChanger;
    [SerializeField] GravitySystem gravitySystem;
    private void Start()
    {
        quoteIntro.onShowTextFinished.AddListener(() => Debug.Log("Show text finished"));
        quoteIntro.onHideBackgroundFinished.AddListener(() => Debug.Log("Hide background finished"));
        quoteIntro.onShowBackgroundFinished.AddListener(() => Debug.Log("Show background finished"));
    }
    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard.hKey.wasPressedThisFrame)
            quoteIntro.ShowTextAndHideBackground();
        if (keyboard.jKey.wasPressedThisFrame)
            quoteIntro.ShowText();
        if (keyboard.kKey.wasPressedThisFrame)
            quoteIntro.HideBackground();
        if (keyboard.lKey.wasPressedThisFrame)
            quoteIntro.ShowBackground();
        if (keyboard.fKey.wasPressedThisFrame)
            sizeChangable.ModifySize(2f);
        if (keyboard.gKey.wasPressedThisFrame)
            sizeChangable.ModifySize(1f);
        if (keyboard.dKey.wasPressedThisFrame)
            gravityChanger.Interact();
        if (keyboard.sKey.wasPressedThisFrame)
            gravitySystem.SwapGravity();
    }
}
