using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTapHandel : MonoBehaviour
{
    private Vector3 pos;

    public Action isTap;
    public Action<int> isTapWithIndex;

    //[SerializeField] private bool withIndex = false;
    [SerializeField] private IAnimalUniqIndex animalUniqIndex;// = null;

    //private void Start()
    //{
    //    if(withIndex)
    //    {
    //        animalUniqIndex = GetComponent<IAnimalUniqIndex>();
    //    }
    //}

    void Update()
    {
#if ( UNITY_IOS || UNITY_ANDROID ) && !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            CheckIsTap();
        }

#elif UNITY_EDITOR || UNITY_WINDOWS || UNITY_MAC
        if (Input.GetMouseButtonUp(0))
        {
            CheckIsTap();
        }
#endif
    }

    void CheckIsTap()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetComponent<CircleCollider2D>().OverlapPoint(pos))
        {
            if (animalUniqIndex != null) isTapWithIndex.Invoke(animalUniqIndex.Index);
            else isTap.Invoke();
        }
    }
}
