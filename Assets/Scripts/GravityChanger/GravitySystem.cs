using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    ICollection<Gravitable> gravitables = new List<Gravitable>();

    Gravitable.Dir currentGravityDir = Gravitable.Dir.Normal;

    public void Subscribe(Gravitable gravitable)
    {
        gravitables.Add(gravitable);
    }

    public void SwapGravity()
    {
        Gravitable.Dir newGravityDir = 
            (currentGravityDir == Gravitable.Dir.Normal) ? Gravitable.Dir.Reverse : Gravitable.Dir.Normal;
        currentGravityDir = newGravityDir;

        foreach (Gravitable gravitable in gravitables)
            gravitable.SwapGravity(newGravityDir);
    }
}
