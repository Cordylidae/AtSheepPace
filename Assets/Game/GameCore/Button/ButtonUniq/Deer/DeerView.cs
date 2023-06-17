public class DeerView : ButtonView
{
    public IAnimalUniqIndex AnimalNumberIndex;
    public IAnimalSign AnimalSign;
    public DeerSwapSign DeerSwapSign;
    public override void SetOpen()
    {
        base.SetOpen();
        AnimalSign.gameObject.SetActive(true);
        AnimalNumberIndex.gameObject.SetActive(true);
        DeerSwapSign.IsOpen = true;
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalSign.gameObject.SetActive(false);
        AnimalNumberIndex.gameObject.SetActive(false);
        DeerSwapSign.IsOpen = false;
    }
}
