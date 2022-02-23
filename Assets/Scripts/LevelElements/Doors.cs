using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] int doorsId;
    [SerializeField] IntEvent onDoorsOpened;

    private void Awake()
    {
        onDoorsOpened.OnEventRaised += OpenIfIdMatches;
    } 

    void OpenIfIdMatches(int doorsId)
    {
        if (doorsId == this.doorsId)
            Open();
    }

    void Open()
    {
        Destroy(gameObject);
    }
}