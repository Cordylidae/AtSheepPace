public class DeerView : ButtonView
{
    public IAnimalUniqIndex AnimalNumberIndex;
    public IAnimalSign AnimalSign;
    public override void SetOpen()
    {
        base.SetOpen();
        AnimalSign.gameObject.SetActive(true);
        AnimalNumberIndex.gameObject.SetActive(true);
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalSign.gameObject.SetActive(false);
        AnimalNumberIndex.gameObject.SetActive(false);
    }
}
