using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleOfSubs : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private GameObject SubButton;

    private int MaxCount = 9;

    private int AllCount;
    public int AnswerCount;
    public int RandCount;

    private SubSignCounter signCounter = new SubSignCounter();
    private List<SubViewBoar> subViews = new List<SubViewBoar>();

    public void MakeSubsBoar(int count)
    {
        if (count > 4) { count = 4; Debug.LogError("Subs of Boar > 4"); }
        if (count < 1) { count = 1; Debug.LogError("Subs of Boar < 1"); }
        AnswerCount = count; RandCount = UnityEngine.Random.Range(0, 100) % (MaxCount - AnswerCount * 2 + 1);
        AllCount = AnswerCount * 2 + RandCount;

        GameObject subList = new GameObject("SubList");
        subList.transform.SetParent(this.transform);
        subList.transform.localPosition = Vector3.zero;

        GameObject sub;

        for (int i = 0; i < AllCount; i++)
        {
            sub = Instantiate(
                            SubButton,
                            Vector3.zero,
                            SubButton.transform.rotation);

            sub.transform.SetParent(subList.transform);
            sub.transform.localPosition = PositionByAngle(360 / AllCount * i);

            SubViewBoar subView = sub.GetComponent<SubViewBoar>();

            if (subView != null)
            {
                subView.BaseTapHandel.isTap += () =>
                {
                    subView.Tapped();
                };
            }

            subViews.Add(subView);
        }

        BoarRandomSetSign();
    }
    private void BoarRandomSetSign()
    {
        subViews = subViews.OrderBy(a => Guid.NewGuid()).ToList();

        for (int i = 0; i < AnswerCount; i++)
        {
            subViews[i].AnimalSign.Sign = SignState.True;
            subViews[i + AnswerCount].AnimalSign.Sign = SignState.False;
        }

        for (int i = 0; i < RandCount; i++)
        {
            subViews[AnswerCount * 2 + i].AnimalSign.SetRandomSign();
        }
    }

    public SubSignCounter GetAnswerBoar()
    {
        signCounter.SubSignCounterReset();

        foreach (SubViewBoar subView in subViews) {
            if (subView.SubChoose.IsChoose)
            {
                signCounter.Count[(int)subView.AnimalSign.Sign]++;
            }
        }

        return signCounter;
    }

    private Vector3 PositionByAngle(float angle)
    {
        Vector3 positionSub = new Vector3(Mathf.Sin(angle * Mathf.PI / 180), Mathf.Cos(angle * Mathf.PI / 180), -0.01f);
        
        positionSub = positionSub.normalized * circleCollider.radius;

        return positionSub;
    }
}

public class SubSignCounter
{
    public int[] Count = new int[2]; // True, False
   
    public SubSignCounter()
    {
        SubSignCounterReset();
    }

    public void SubSignCounterReset()
    {
        Count[0] = 0;
        Count[1] = 0;
    }
};
