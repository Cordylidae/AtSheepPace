using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSpaceOfSubs : MonoBehaviour
{
    [SerializeField] private GameObject SubButton;

    [NonSerialized] public List<SubView> subViews = new List<SubView>();

    private int MaxCount = 7;
    public void MakeSubs(int count)
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

            SubView subView = sub.GetComponent<SubView>();

            if (subView != null)
            {
                subView.BaseTapHandel.isTap += () =>
                {
                    subView.Tapped();
                };
            }

            subViews.Add(subView);
        }

        //RandomSetSign();
    }
}
