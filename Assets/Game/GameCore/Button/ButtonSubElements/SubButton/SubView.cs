using UnityEngine;

public class SubView : TapView
{
    public IAnimalSign AnimalSign;
    public ISubChoose SubChoose;

    public void Tapped()
    {
        SubChoose.IsChoose = !SubChoose.IsChoose;

        if(SubChoose.IsChoose)
        {
            Choose();
        }else UnChoose();
    }

    public void Choose()
    {
        SubChoose.IsChoose = true;

        this.gameObject.transform.localScale = Vector3.one * 1.2f;
    }

    public void UnChoose()
    {
        SubChoose.IsChoose = false;

        this.gameObject.transform.localScale = Vector3.one;
    }

    private void OnDestroy()
    {
        BaseTapHandel.ResetSubscriptions();
    }
}
