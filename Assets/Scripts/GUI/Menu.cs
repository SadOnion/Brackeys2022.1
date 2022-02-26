using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button playBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] LeanTweenType fadeOutType;

    public UnityEvent onPlay = new UnityEvent();

    private void Start()
    {
        playBtn.onClick.AddListener(Hide);
        exitBtn.onClick.AddListener(Application.Quit);
    }

    void Hide()
    {
        canvasGroup.LeanAlpha(0f, 0.6f)
            .setEase(fadeOutType)
            .setOnComplete(() => gameObject.SetActive(false));
    }
}
