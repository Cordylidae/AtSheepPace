using DG.Tweening;
using System;
using UnityEngine;

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
        Debug.Log(this.transform.parent.name + " " + fearBar._currentProgress + " " + fearBar.ProportionOfProgress);
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
    public float _currentProgress
    {
        get => currentProgress;
        private set
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
        _currentProgress -= 3.0f;
    }

    public void SheepBad()
    {
        _currentProgress += 5.0f;
    }

    public void WolfGood()
    {
        _currentProgress -= 0.0f;
    }

    public void WolfBad()
    {
        _currentProgress += 28.0f;
    }

    public void DeerGood(string dayTime)
    {
        if(dayTime == RuleDayTime.Time.Sun) _currentProgress -= 18.0f;
        else _currentProgress -= 12.0f;
    }

    public void DeerBad(string dayTime)
    {
        if (dayTime == RuleDayTime.Time.Sun) _currentProgress += 25.0f;
        else _currentProgress += 17.0f;
    }

    public void BoarGood(string dayTime)
    {
        if (dayTime == RuleDayTime.Time.Moon) _currentProgress -= 24.0f;
        else _currentProgress -= 6.0f;
    }

    public void BoarBad(string dayTime)
    {
        if (dayTime == RuleDayTime.Time.Sun) _currentProgress += 10.0f;
        else _currentProgress += 32.0f;
    }

    public void HedgehogGood(string dayTime)
    {
        if (dayTime == RuleDayTime.Time.Moon) _currentProgress -= 25.0f;
        else _currentProgress -= 15.0f;
    }

    public void HedgehogBad(string dayTime)
    {
        if (dayTime == RuleDayTime.Time.Sun) _currentProgress += 22.0f;
        else _currentProgress += 28.0f;
    }
};
