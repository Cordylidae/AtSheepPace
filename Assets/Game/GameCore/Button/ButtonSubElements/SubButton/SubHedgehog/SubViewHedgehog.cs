using UnityEngine;
using DG.Tweening;

public class SubViewHedgehog : SubView
{
    public LineRenderer lineRenderer;
    public IAnimalColor animalColor;
    public IAnimalForm animalForm;

    private Tween tween;

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
            Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

            colorStart = colorEnd = color;
            //lineRenderer.startColor;

            colorStart.a = 1.0f;
            colorEnd.a = 0.0f;

            tween = lineRenderer.DOColor(new Color2(colorStart, colorStart), new Color2(colorEnd, colorEnd), 0.5f).SetLoops(2,LoopType.Restart);
        }
    }

    public void DisableLine()
    {
        tween.Kill();
        tween = null;

        lineRenderer.gameObject.SetActive(false);
    }
}
