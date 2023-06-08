using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTapHandel : MonoBehaviour
{
    public event Action isTap;

    public void Tapped()
    {
        isTap?.Invoke();
    }
}
