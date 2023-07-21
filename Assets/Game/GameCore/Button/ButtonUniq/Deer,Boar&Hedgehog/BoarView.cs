public class BoarView : ButtonView
{
    public IAnimalUniqIndex AnimalNumberIndex;
    public CircleOfSubs circleOfSubs;

    public override void SetOpen()
    {
        base.SetOpen();
        AnimalNumberIndex.gameObject.SetActive(true);
        circleOfSubs.gameObject.SetActive(true);
    }
    public override void SetClose()
    {
        base.SetClose();
        AnimalNumberIndex.gameObject.SetActive(false);
        circleOfSubs.gameObject.SetActive(false);
    }
}
