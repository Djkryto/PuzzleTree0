public class ReadingObject : InteractiveItem, IReadable
{
    public override IReadable Readable => this;
    public override IPortable Portable => null;
    public override ITakeable Takeable => null;
    public override IInspectable Inspectable => null;
    public override IUseable Useable => null;

    public void Read()
    {
        throw new System.NotImplementedException();
    }
}
