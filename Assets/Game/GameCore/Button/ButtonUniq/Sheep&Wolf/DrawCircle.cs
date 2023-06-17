using DG.Tweening;
using System;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public System.Action<int> isGoneRadius;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int vertex;
    [SerializeField] private float radius;
    [SerializeField] private float widthCircle = 0.1f;

    [SerializeField] private Transform goodRange;
    [SerializeField] private Transform badRange;


    private Color myColor = new Color(1.0f, 1.0f, 1.0f, 0.0f); 
    private Tween tween;

    public Action radiusZero;
    public void ResetSubscriptions() => radiusZero = null;

    // ## Need to make its gloanbal
    private float globalScale = 0.8f;

    // ### NEED to draw onther way to chose point foir radius with GIzmos
    // ### NEED fix this shit
    public float Radius 
    {
        get
        {
            return radius / 10.0f;
        }
        private set
        {
            radius = value; DrawPolygon(vertex, Radius, this.transform.position, widthCircle, widthCircle);

            if (Radius < goodRange.localPosition.x * globalScale && Radius >= badRange.localPosition.x * globalScale) myColor = new Color(0.6f, 1.0f, 0.8f, 1.0f);
            if (Radius < badRange.localPosition.x * globalScale) myColor = new Color(1.0f, 0.84f, 0.0f, 1.0f);

            if (Radius <= badRange.localPosition.x * globalScale * 0.6f) radiusZero?.Invoke();
        }
    }

    private void Awake()
    {
        DrawPolygon(vertex, Radius, this.transform.position, widthCircle, widthCircle);
    }

    public void StartDrawing(float timeChange)
    {
        myColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        tween = DOTween.To(x => Radius = x, radius, 0, timeChange);
    }

    public void StopDrawing()
    {
        tween.Kill();
    }

    void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));
        }

        lineRenderer.SetColors(myColor, myColor);
    }
}
