using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quote", menuName = "ScriptableObjects/Quote", order = 1)]
public class QuoteSO : ScriptableObject
{
    [SerializeField] string text = "Placeholder quote";
    public string Text => text;
    [SerializeField] float displayTime = 1f;
    public float DisplayTime => displayTime;

    private void OnValidate()
    {
        displayTime = Mathf.Max(0, displayTime);
    }
}
