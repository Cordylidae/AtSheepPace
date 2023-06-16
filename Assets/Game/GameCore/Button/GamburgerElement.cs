using System;
using System.Collections.Generic;

public class GamburgerElement
{
    public BaseElement baseE;
    public List<AdditionalElement> additionE;

    public GamburgerElement(BaseElement baseElement)
    {
        baseE = baseElement;
        additionE = new List<AdditionalElement>();
    }
}

public abstract class Element
{
    public readonly string animalType;

    public Action IOpen;
    protected bool isOpen;
    public void ResetSubscriptions() => IOpen = null;
    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; IOpen?.Invoke(); }
    }

    public Element(string type, bool open = true)
    {
        animalType = type;
        isOpen = open;
    }
}

public class BaseElement : Element
{
    public readonly int number; // can be some sheeps with same number

    public BaseElement(string type, int num, bool open = true) : base(type, open)
    {
        number = num;
    }
}

public class AdditionalElement : Element
{
    public AdditionalElement(string type, bool open = false) : base(type, open) 
    {
    }
}
