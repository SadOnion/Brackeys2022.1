using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : DefaultCollectible
{
    [SerializeField] int openedDoorsId;
    [SerializeField] IntEvent onKeyCollected;

    public override void Collect()
    {
        onKeyCollected.RaiseEvent(openedDoorsId);
    }
}
