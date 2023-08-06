using System;
using UnityEngine;

public abstract class TapView : MonoBehaviour
{
    public BaseTapHandel BaseTapHandel;
}

public abstract class ButtonView : TapView
{
    public IAnimalType AnimalType;
    public IAnimalOpenView AnimalOpenView;

    public virtual void SetOpen()
    {
        AnimalOpenView.IsOpen = true;
    }
    public virtual void SetClose()
    {
        AnimalOpenView.IsOpen = false;
    }
}

public abstract class BaseButtonView : ButtonView
{
    public IAnimalUniqIndex AnimalUniqIndex;
    public DrawCircle DrawCircle;

    public override void SetOpen()
    {
        base.SetOpen();
        AnimalUniqIndex.gameObject.SetActive(true);
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalUniqIndex.gameObject.SetActive(false);
    }
}