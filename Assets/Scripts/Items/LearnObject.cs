using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnObject : InteractiveItem, ILearn
{
    public override IPortable Portable => throw new System.NotImplementedException();
    public override ITakeable Takeable => throw new System.NotImplementedException();
    public override IInspectable Inspectable => throw new System.NotImplementedException();
    public override ILearn Learn => this;
    public override IUseable Useable => throw new System.NotImplementedException();
    public override IReading Reading => throw new System.NotImplementedException();
}
