using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FearBarView : MonoBehaviour
{
    //public Gradient Gradient;
    // Color c = Gradient.Evaluate(t);

    [SerializeField] RectTransform bar;
    [SerializeField] Transform arrow;

    [SerializeField] private List<Transform> borderPoints; 
 
    public FearBar fearBar = new FearBar();

    private void Awake()
    {
        fearBar.changeProgress += ChangeProgress;
    }

    void ChangeProgress()
    {
        Vector3 newPositionArrow = arrow.localPosition;

        if (fearBar.currentBorderIndex >= fearBar.maxBorderIndex) { newPositionArrow = new Vector3(bar.rect.width, arrow.localPosition.y, arrow.localPosition.z); }
        else
        {
            float xProportion = fearBar.ProportionOfProgress();
            float xStartWidth = borderPoints[fearBar.currentBorderIndex].localPosition.x;
            float xPartDist = borderPoints[fearBar.currentBorderIndex + 1].localPosition.x - borderPoints[fearBar.currentBorderIndex].localPosition.x;

            Debug.Log("Proportion: " + xProportion + ", Start Point: " + xStartWidth + ", Part Distance: " + xPartDist + ", Index: " + fearBar.currentBorderIndex);

            newPositionArrow = new Vector3(xStartWidth + xProportion * xPartDist, arrow.localPosition.y, arrow.localPosition.z);
        }

        arrow.DOLocalMove(newPositionArrow, 1, true).OnComplete(() => fearBar.checkOnFull());
    }

    private void OnDestroy()
    {
        fearBar.changeProgress -= ChangeProgress;
    }
}


// ### NEED Initialaize like INJECT 
public class FearBar
{
    private float fullSizeProgress = 140.0f;
    private float currentProgress = 0.0f;

    public int currentBorderIndex {get; private set;} = 0;
    private float[] bordersPercent = { 0.0f, 10.0f, 30.0f, 70.0f, 90.0f, 100.0f }; // need be synchronize with points of FearBarView
    private float BordersPercent(int index) => bordersPercent[index] / 100.0f;
    public int maxBorderIndex => bordersPercent.Length - 1;

    public Action changeProgress;
    public Action fullFearBar;
    public float _currentProgress
    {
        get => currentProgress;
        private set
        {
            if (value > PercentInValue(maxBorderIndex))
            {
                currentProgress = PercentInValue(maxBorderIndex);
                currentBorderIndex = maxBorderIndex;
                // need end game
            }
            else if (value < PercentInValue(currentBorderIndex))
            {
                currentProgress = PercentInValue(currentBorderIndex);
            }
            else
            {
                currentProgress = value;
                currentBorderIndex = ValueInPercent(value);
            }

            changeProgress?.Invoke();
        }
    }

    public void checkOnFull()
    {
        if (currentBorderIndex >= maxBorderIndex)
        {
            Debug.Log("You lose");
            fullFearBar?.Invoke();
        }
    }

    public float ProportionOfProgress()
    {
        int index = currentBorderIndex;
        if (index + 1 > maxBorderIndex) index--;

        float curPercent = currentProgress / fullSizeProgress;
        float localPercent = (curPercent - BordersPercent(index));

        float maxLocalPercent = BordersPercent(index + 1) - BordersPercent(index);

        return (localPercent / maxLocalPercent);
    }
    private float PercentInValue(int index)
        => bordersPercent[index] * fullSizeProgress / 100.0f;

    private int ValueInPercent(float value)
    {
        for (int i = currentBorderIndex; i < maxBorderIndex; i++)
        {
            if (value >= PercentInValue(i) && value < PercentInValue(i+1))
            {
                return i;
            }
        }
        return maxBorderIndex;
    }

    public FearBar ()
    {
        currentBorderIndex = 0;
    }

    public void Good(string animalType, string dayTime)
    {
        switch (animalType)
        {
            case AnimalType.Sheep: 
                {
                    _currentProgress -= 5.0f;
                } 
                return;
            case AnimalType.Wolf:
                {
                    _currentProgress -= 0.0f;
                } 
                return;
            case AnimalType.Deer:
                {
                    if (dayTime == RuleDayTime.Time.Sun) _currentProgress -= 15.0f;
                    else _currentProgress -= 10.0f;
                } 
                return;
            case AnimalType.Boar:
                {
                    if (dayTime == RuleDayTime.Time.Moon) _currentProgress -= 25.0f;
                    else _currentProgress -= 5.0f;
                } 
                return;
            case AnimalType.Hedgehog:
                {
                    if (dayTime == RuleDayTime.Time.Moon) _currentProgress -= 25.0f;
                    else _currentProgress -= 15.0f;
                }
                return;
        }

        throw new NotImplementedException();
    }

    public void Bad(string animalType, string dayTime)
    {
        switch (animalType)
        {
            case AnimalType.Sheep:
                {
                    _currentProgress += 7.0f;
                }
                return;
            case AnimalType.Wolf:
                {
                    _currentProgress += 28.0f;
                }
                return;
            case AnimalType.Deer:
                {
                    if (dayTime == RuleDayTime.Time.Sun) _currentProgress += 25.0f;
                    else _currentProgress += 15.0f;
                }
                return;
            case AnimalType.Boar:
                {
                    if (dayTime == RuleDayTime.Time.Sun) _currentProgress += 10.0f;
                    else _currentProgress += 30.0f;
                }
                return;
            case AnimalType.Hedgehog:
                {
                    if (dayTime == RuleDayTime.Time.Sun) _currentProgress += 15.0f;
                    else _currentProgress += 25.0f;
                }
                return;
        }

        throw new NotImplementedException();
    }
};
