using UnityEngine;

public abstract class ButtonView : MonoBehaviour
{
    public IAnimalType AnimalType;
    public BaseTapHandel BaseTapHandel;
}

public abstract class BaseButtonView : ButtonView
{
    public bool isOpen;
    public IAnimalUniqIndex AnimalUniqIndex;
    public DrawCircle DrawCircle;
}