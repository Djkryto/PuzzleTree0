using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingObject : InteractiveItem, IReading
{
    public override IReading Reading => this;
    public override IPortable Portable => throw new System.NotImplementedException();

    public override ITakeable Takeable => throw new System.NotImplementedException();

    public override IInspectable Inspectable => throw new System.NotImplementedException();

    public override ILearn Learn => throw new System.NotImplementedException();
    public override IUseable Useable => throw new System.NotImplementedException();
}
