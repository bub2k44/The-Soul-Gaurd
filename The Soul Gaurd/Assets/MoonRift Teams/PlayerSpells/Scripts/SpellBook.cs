using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpellBook : MonoBehaviour
{
    private static SpellBook instance;

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Image castingBarFill;

    [SerializeField]
    private TextMeshProUGUI currentSpell;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI castTime;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Spell[] spells;

    private Coroutine spellroutine;

    private Coroutine fadeRoutine;

    public Spell CastSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);
        castingBarFill.fillAmount = 0;
        //canvasGroup.alpha = 
        castingBarFill.color = spell.MyBarColor;
        currentSpell.text = spell.MyName;
        icon.sprite = spell.MyIcon;
        spellroutine = StartCoroutine(Progress(spell));
        fadeRoutine = StartCoroutine(FadeBar());
        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float timePassed = Time.deltaTime;
        float rate = 1.0f / spell.MyCastTime;
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            castingBarFill.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            timePassed += Time.deltaTime;
            castTime.text = (spell.MyCastTime - timePassed).ToString("F2");

            if (spell.MyCastTime - timePassed < 0)
            {
                castTime.text = "0.00";
            }

            yield return null;
        }

        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.5f;
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }
    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }
        if (spellroutine != null)
        {
            StopCoroutine(spellroutine);
            spellroutine = null;
        }
    }

    public Spell GetSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);
        return spell;
    }
}
