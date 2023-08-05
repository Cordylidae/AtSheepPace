using System;
using UnityEngine;

public class PanelView : MonoBehaviour
{
    public Action CloseTapped;
    public Action StartLevelTapped;

    public void CloseTap()
    {
        CloseTapped?.Invoke();
    }

    public void StartLevelTap()
    {
        StartLevelTapped?.Invoke();
    }
}
