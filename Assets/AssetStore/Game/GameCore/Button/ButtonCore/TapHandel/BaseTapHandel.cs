using System;
using UnityEngine;

public class BaseTapHandel : MonoBehaviour
{
    public Action isTap;
    public void ResetSubscriptions() => isTap = null;

    public void Tapped()
    {
        isTap?.Invoke();
    }
}
