using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnObject : InteractiveItem, ILearn
{
    public override IPortable Portable => null;
    public override ITakeable Takeable => null;
    public override IInspectable Inspectable => null;
    public override ILearn Learn => this;
    public override IUseable Useable => null;
    public override IReading Reading => null;
}
