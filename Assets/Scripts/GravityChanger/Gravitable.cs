using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Gravitable
{
    public enum Dir { Normal, Reverse }

    public void SwapGravity(Dir gravityDir);
}
