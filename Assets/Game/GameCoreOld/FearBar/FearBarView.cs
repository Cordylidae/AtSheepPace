using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearBarView : MonoBehaviour
{
    //public Gradient Gradient;
    // Color c = Gradient.Evaluate(t);

    [SerializeField] RectTransform bar;
    [SerializeField] Transform arrow;

    public FearBar fearBar = new FearBar();

    private void Awake()
    {
        fearBar.changeProgress += ChangeProgress;
    }

    void ChangeProgress()
    {
        arrow.DOLocalMove(new Vector3(bar.rect.width * fearBar.ProportionOfProgress, arrow.localPosition.y, arrow.localPosition.z), 1, true);
    }

    private void OnDestroy()
    {
        fearBar.changeProgress -= ChangeProgress;
    }
}


// ### NEED Initialaize like INJECT 
public class FearBar
{
    private float fullSizeProgress = 120.0f;
    private float currentProgress = 0.0f;
    private float _currentProgress
    {
        get => currentProgress;
        set
        {
            if (value < 0.0f)
            {
                currentProgress = 0.0f;
            }
            else if (value > fullSizeProgress)
            {
                currentProgress = fullSizeProgress;
            }
            else currentProgress = value;

            changeProgress?.Invoke();
        }
    }

    public float ProportionOfProgress => currentProgress / fullSizeProgress;

    public Action changeProgress; 

    public FearBar ()
    { 
    }

    public void SheepGood()
    {
        _currentProgress -= 7.0f;
    }

    public void SheepBad()
    {
        _currentProgress += 15.0f;
    }

    public void WolfGood()
    {
        _currentProgress -= 0.0f;
    }

    public void WolfBad()
    {
        _currentProgress += 48.0f;
    }
};
