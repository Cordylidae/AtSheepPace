using UnityEngine;

public abstract class SubView : TapView
{
    public ISubChoose SubChoose;

    public void Tapped()
    {
        SubChoose.IsChoose = !SubChoose.IsChoose;

        if(SubChoose.IsChoose)
        {
            Choose();
        }else UnChoose();
    }

    public virtual void Choose()
    {
        SubChoose.IsChoose = true;

        this.gameObject.transform.localScale = Vector3.one * 1.2f;
    }

    public virtual void UnChoose()
    {
        SubChoose.IsChoose = false;

        this.gameObject.transform.localScale = Vector3.one;
    }

    private void OnDestroy()
    {
        BaseTapHandel.ResetSubscriptions();
    }
}
