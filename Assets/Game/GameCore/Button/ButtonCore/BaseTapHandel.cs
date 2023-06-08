using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTapHandel : MonoBehaviour
{
    private Vector3 pos;

    public event Action isTap;

    void Update()
    {
#if ( UNITY_IOS || UNITY_ANDROID ) && !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            CheckTapThis();
        }

#elif UNITY_EDITOR || UNITY_WINDOWS || UNITY_MAC
        if (Input.GetMouseButtonUp(0))
        {
            CheckTapThis();
        }
#endif
    }

    void CheckTapThis()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetComponent<CircleCollider2D>().OverlapPoint(pos))
        {
            Debug.Log(this.transform.parent.name);
            isTap.Invoke();
        }
    }
}
