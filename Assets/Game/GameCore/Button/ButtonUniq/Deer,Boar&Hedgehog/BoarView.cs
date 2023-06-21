public class BoarView : ButtonView
{
    public IAnimalUniqIndex AnimalNumberIndex;
    public override void SetOpen()
    {
        base.SetOpen();
        AnimalNumberIndex.gameObject.SetActive(true);
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalNumberIndex.gameObject.SetActive(false);
    }
}
