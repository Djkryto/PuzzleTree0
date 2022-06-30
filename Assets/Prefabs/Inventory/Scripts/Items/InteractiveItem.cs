﻿using UnityEngine;

public abstract class InteractiveItem : MonoBehaviour
{
    public abstract IPortable Portable { get; }
    public abstract ITakeable Takeable { get; }
    public abstract IInspectable Inspectable { get; }
    public abstract IUseable Useable { get; }
}