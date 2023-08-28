using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class DrawCircleUndlessMenu : MonoBehaviour
{
    public System.Action<int> isGoneRadius;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int vertex;
    [SerializeField] private float startRadius;
    [SerializeField] private float startWidthCircle;
   
    [SerializeField] private float timeShow = 5.0f;
    [SerializeField] private float timeWait = 5.0f;

    [SerializeField] private Transform goodRange;
    [SerializeField] private Transform badRange;


    private Color myColor;

    private Tween radiusTween;
    private float radius;
    private float widthCircle;

    public void ResetSubscriptions()
    {
        radiusTween.Kill();
        radiusTween = null;
    }
    // ## Need to make its gloanbal
    private float globalScale = 1.0f / ((1080.0f) / (1080.0f * 0.0052f));

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
            if (widthCircle > 0.08f) widthCircle -= 0.02f * Time.deltaTime;

            if (Radius < goodRange.localPosition.x * globalScale && Radius >= badRange.localPosition.x * globalScale) myColor = new Color(0.6f, 1.0f, 0.8f, 1.0f);
            if (Radius < badRange.localPosition.x * globalScale) myColor -= new Color(0.0f, 0.0f, 0.0f, 4.0f * Time.deltaTime);
            if (Radius < badRange.localPosition.x * globalScale * 0.625f) StartAgain();
        }
    }

    private void Awake()
    {
        myColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        Radius = startRadius;
        widthCircle = startWidthCircle;
    }

    public void GetStart()
    {
        StartDrawing(timeShow);
    }

    async private void StartAgain()
    {
        ResetSubscriptions();

        myColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        DrawPolygon(vertex, Radius, this.transform.position, widthCircle, widthCircle);        

        await Task.Delay((int)(timeWait * 1000));

        radius = startRadius;
        widthCircle = startWidthCircle;

        StartDrawing(timeShow);
    }

    private void StartDrawing(float timeChange)
    {
        myColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        radiusTween = DOTween.To(x => Radius = x, radius, 0, timeChange);
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
