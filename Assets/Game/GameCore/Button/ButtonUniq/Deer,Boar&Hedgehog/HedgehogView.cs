public class HedgehogView : ButtonView
{
    public IAnimalSign AnimalSign;
    public FreeSpaceOfSubs freeSpaceOfSubs;
    public override void SetOpen()
    {
        base.SetOpen();
        AnimalSign.gameObject.SetActive(true);
        freeSpaceOfSubs.gameObject.SetActive(true);
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalSign.gameObject.SetActive(false);
        freeSpaceOfSubs.gameObject.SetActive(false);
    }
}