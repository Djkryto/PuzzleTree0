public class ReadingObject : InteractiveItem, IReading
{
    public override IReading Reading => this;
    public override IPortable Portable => null;

    public override ITakeable Takeable => null;

    public override IInspectable Inspectable => null;

    public override ILearn Learn => null;
    public override IUseable Useable => null;
}
