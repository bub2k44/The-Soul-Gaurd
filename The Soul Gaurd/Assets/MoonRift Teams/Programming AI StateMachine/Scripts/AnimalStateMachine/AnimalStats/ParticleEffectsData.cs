using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ParticleEffectsData")]
public class ParticleEffectsData : ScriptableObject
{
    public ParticleSystem sleepyFX;
    public ParticleSystem playFX;
    public ParticleSystem attackFX;
    public ParticleSystem damageFX;
}