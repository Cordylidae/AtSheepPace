public class HedgehogView : ButtonView
{
    public IAnimalSign AnimalSign;
    public override void SetOpen()
    {
        base.SetOpen();

        AnimalSign.gameObject.SetActive(true);
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalSign.gameObject.SetActive(false);
    }
}