using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Totem
{
    public enum Type {Red, Green, Blue}

    public Type type { get; private set; }
    public GameObject totem { get; private set; }

    public Totem(Type type, GameObject totem)
    {
        this.type = type;
        this.totem = totem;
    }
}
