using UnityEngine;
using DG.Tweening;

public class SubViewHedgehog : SubView
{
    public LineRenderer lineRenderer;
    public IAnimalColor animalColor;
    public IAnimalForm animalForm;

    public override void Choose()
    {
        base.Choose();
        ShowLine();
    }

    public override void UnChoose()
    {
        base.UnChoose();
    }

    public void ShowLine()
    {
        lineRenderer.gameObject.SetActive(true);   
        if (lineRenderer != null)
        {
            Color colorStart, colorEnd;
            colorStart = colorEnd = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            //lineRenderer.startColor;
            colorStart.a = 1.0f;
            colorEnd.a = 0.0f;

            lineRenderer.DOColor(new Color2(colorStart, colorStart), new Color2(colorEnd, colorEnd), 2.0f);
        }
    }

    public void DisableLine()
    {
        lineRenderer.gameObject.SetActive(false);
    }
}
