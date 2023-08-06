using System;
using UnityEngine;

public class IUniqIndex : MonoBehaviour
{
    [SerializeField] private int index = 0;
    public int Index
    {
        set
        {
            index = value;
            if (index < 0) throw new Exception();
        }
        get
        {
            return index;
        }
    }
}
