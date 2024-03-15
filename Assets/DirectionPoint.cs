using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPoint : MonoBehaviour
{
    public Transform NextBlock = null;

    public bool IsNextBlockSet()
    {
        return NextBlock != null;
    }
}
