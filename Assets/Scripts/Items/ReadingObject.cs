public class ReadingObject : InteractiveItem, IReadable
{
    public override IReadable Readable => this;
    public override IPortable Portable => null;
    public override ITakeable Takeable => null;
    public override IInspectable Inspectable => null;
    public override IUseable Useable => null;

    public void ReadText()
    {
        throw new System.NotImplementedException();
    }

    public void CloseText()
    {
        throw new System.NotImplementedException();
    }
}
