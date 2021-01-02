using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Spell : IUseable, IMoveable
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private Color barColor;

    public string MyName { get => name; }
    public int MyDamage { get => damage; }
    public Sprite MyIcon { get => icon; }

    //public Sprite MyICon => throw new NotImplementedException();

    public float MySpeed { get => speed; }
    public float MyCastTime { get => castTime; }
    public GameObject MySpellPrefab { get => spellPrefab; }
    public Color MyBarColor { get => barColor; }

    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }
}
