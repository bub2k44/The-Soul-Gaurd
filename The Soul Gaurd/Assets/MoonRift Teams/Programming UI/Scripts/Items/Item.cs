using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    private SlotScript slot;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    public Sprite MyIcon { get => icon; }
    public int MyStackSize { get => stackSize; }
    protected SlotScript Slot { get => slot; set => slot = value; }
}