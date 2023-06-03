using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private float widthCircle = 0.1f;

    void Update()
    {
        lineRenderer.startWidth = widthCircle;
        lineRenderer.endWidth = widthCircle;
    }
}
