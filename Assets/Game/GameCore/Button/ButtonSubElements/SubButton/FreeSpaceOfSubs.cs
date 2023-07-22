using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSpaceOfSubs : MonoBehaviour
{
    [SerializeField] private GameObject SubButton;

    [NonSerialized] public List<SubViewHedgehog> subViews = new List<SubViewHedgehog>();

    private int MaxCount = 7;
    public void MakeSubsHedgehog(int count)
    {
        if (count > 7) { count = 7; Debug.LogError("Subs of Hedgehog > 7"); }
        if (count < 2) { count = 2; Debug.LogError("Subs of Hedgehog < 2"); }
        //AnswerCount = count; RandCount = UnityEngine.Random.Range(0, 100) % (MaxCount - AnswerCount * 2 + 1);
        //AllCount = AnswerCount * 2 + RandCount;

        GameObject subList = new GameObject("SubList");
        subList.transform.SetParent(this.transform);
        subList.transform.localPosition = Vector3.zero;

        GameObject sub;

        for (int i = 0; i < count; i++)
        {
            sub = Instantiate(
                            SubButton,
                            Vector3.zero,
                            SubButton.transform.rotation);

            sub.transform.SetParent(subList.transform);

            SubViewHedgehog subView = sub.GetComponent<SubViewHedgehog>();

            if (subView != null)
            {
                subView.BaseTapHandel.isTap += () =>
                {
                    subView.Tapped();
                };
            }

            subViews.Add(subView);
        }

        HedgehogSetLineColor();
        //RandomSetSign();
    }

    private void HedgehogSetLineColor()
    {
        foreach (SubViewHedgehog view in subViews)
        {
            Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

            view.lineRenderer.startColor = color;
            view.lineRenderer.endColor = color;
        }
    }

    public void ShowLines()
    {
        foreach (SubViewHedgehog view in subViews)
        {
            view.ShowLine();
        }
    }

    public void DisableLines()
    {
        foreach (SubViewHedgehog view in subViews)
        {
            view.DisableLine();
        }
    }
}
