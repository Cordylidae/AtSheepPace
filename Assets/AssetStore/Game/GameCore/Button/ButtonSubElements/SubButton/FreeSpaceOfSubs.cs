using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FreeSpaceOfSubs : MonoBehaviour
{
    [SerializeField] private GameObject SubButton;
    [NonSerialized] public List<SubViewHedgehog> subViews = new List<SubViewHedgehog>();

    SubColorFormCounter subColorFormCounter = new SubColorFormCounter();
    List<ValueTuple<int,int>> subElements = new List<ValueTuple<int,int>>();

    private int BaseCount = 3;
    private int AdditionCount = 0;
    private int MaxCount = 7;
    public void MakeSubsHedgehog(FormState form, ColorState color)
    {
        AdditionCount = UnityEngine.Random.Range(0, 100) % (MaxCount - BaseCount + 1);

        HedgehogSetBaseVar(form, color);
        HedgehogSetAddVar();
        
        GameObject subList = new GameObject("SubList");
        subList.transform.SetParent(this.transform);
        subList.transform.localPosition = Vector3.zero;

        GameObject sub;

        for (int i = 0; i < BaseCount+AdditionCount; i++)
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

        HedgehogSetViewParm();
        HedgehogSetLineColor();
    }

    private void HedgehogSetBaseVar(FormState form, ColorState color)
    {
        List<int> randForms = new List<int>() { 0, 1, 2 };
        List<int> randColors = new List<int>() { 0, 1, 2, 3 };
        
        int F, C;

        subElements.Add(((int)form, (int)color));

        randForms.Remove((int)form);
        randColors.Remove((int)color);

        C = UnityEngine.Random.Range(0, 3);

        subElements.Add(((int)form, randColors[C]));

        randColors.Remove(C);

        F = UnityEngine.Random.Range(0, 2);
        C = UnityEngine.Random.Range(0, 2);

        subElements.Add((randForms[F], randColors[C]));
    }

    private void HedgehogSetAddVar()
    {
        int F, C; 

        for (int i = 0; i < AdditionCount; i++)
        {
            F = UnityEngine.Random.Range(0, 3);
            C = UnityEngine.Random.Range(0, 4);

            subElements.Add((F, C));
        }
    }

    private void HedgehogSetViewParm()
    {
        subViews = subViews.OrderBy(a => Guid.NewGuid()).ToList();

        for (int i = 0; i < BaseCount+AdditionCount; i++)
        {
            subViews[i].animalForm.Form = (FormState)subElements[i].Item1;
            subViews[i].animalColor.myColor = (ColorState)subElements[i].Item2;
        }
    }

    private void HedgehogSetLineColor()
    {
        foreach (SubViewHedgehog view in subViews)
        {
            view.lineRenderer.startColor = view.lineRenderer.endColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
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

    [Button]
    public SubColorFormCounter GetAnswerHedgehog()
    {
        subColorFormCounter.SubSignCounterReset();

        foreach (SubViewHedgehog subView in subViews)
        {
            if (subView.SubChoose.IsChoose)
            {
                subColorFormCounter.Count
                    [(int)subView.animalForm.Form,
                    (int)subView.animalColor.myColor]++;
                
                subColorFormCounter.AllCount++;
            }
        }

        // # Have unreproduced bug with complite wiout right answer
        // Debug.Log(subColorFormCounter.AllCount + " All count");

        return subColorFormCounter;
    }
}

public class SubColorFormCounter
{
    public int AllCount = 0;
    public int[,] Count = new int[3,4]; // form I, color J
    // Circle, Triangle, Hex (Form)
    // Red, Yellow, Green, Blue (Color)

    public SubColorFormCounter()
    {
        SubSignCounterReset();
    }

    public void SubSignCounterReset()
    {
        int[,] ints = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        Count = ints;

        AllCount = 0;
    }
    public int getColorsCount(ColorState colorState)
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            count += Count[i, (int)colorState];
        }

        return count;
    }

    public int getFormCount(FormState formState)
    {
        int count = 0;

        for (int j = 0; j < 4; j++)
        {
            count += Count[(int)formState, j];
        }

        return count;
    }

    public int getFormAndColorCount(FormState formState, ColorState colorState)
    {
        return Count[(int)formState, (int)colorState];
    }
}