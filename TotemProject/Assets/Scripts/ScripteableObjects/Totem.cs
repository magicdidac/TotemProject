using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem
{
    public enum Type {Red, Green, Blue};

    public GameObject totem = null;
    public Type type;

    public Totem(Type type, GameObject totem)
    {
        this.totem = totem;
        this.type = type;
    }
}
