public class HedgehogView : ButtonView
{
    public IAnimalSign AnimalSign;
    public IAnimalForm AnimalForm;
    public IAnimalColor AnimalColor;

    public FreeSpaceOfSubs freeSpaceOfSubs;
    public override void SetOpen()
    {
        base.SetOpen();
        AnimalSign.gameObject.SetActive(true);
        AnimalForm.gameObject.SetActive(true);
        AnimalColor.gameObject.SetActive(true);

        freeSpaceOfSubs.gameObject.SetActive(true);
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalSign.gameObject.SetActive(false);
        AnimalForm.gameObject.SetActive(false);
        AnimalColor.gameObject.SetActive(false);

        freeSpaceOfSubs.gameObject.SetActive(false);
    }
}