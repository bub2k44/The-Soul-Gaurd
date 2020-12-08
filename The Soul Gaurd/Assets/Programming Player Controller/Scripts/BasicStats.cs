using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="newPlayerData",menuName="Data/Player Data/Base Data")]
public class BasicStats : ScriptableObject
{
    [Header("Stats")]
    public string name;
    public float health, movespeed, flyingMovespeed, damage, attackspeed, range;
    public bool possessed;
    public string specialAbility;//later make this an enum
    public void setPossessed(bool val)
    {
        possessed = val;
    }
    public void setHealth(float val)
    {
        health = val;
    }
    public void setDamage(float val)
    {
        damage = val;
    }
    public void setRange(float val)
    {
        range = val;
    }
    public void modHealth(float val)
    {
        health -= val;
    }
    //[Header("Move State")]
}
