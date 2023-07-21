using UnityEngine;

public class SubView : TapView
{
    public IAnimalSign AnimalSign;
    public ISubChoose SubChoose;

    public void Tapped()
    {
        SubChoose.IsChoose = !SubChoose.IsChoose;
    }

    public void Choose()
    {
        SubChoose.IsChoose = true;
    }

    public void UnChoose()
    {
        SubChoose.IsChoose = false;
    }
}
