using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Image castingBarFill;

    [SerializeField]
    private TextMeshProUGUI spellName;

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

    public Spell CastSpell(int index)
    {
        castingBarFill.fillAmount = 0;
        //canvasGroup.alpha = 
        castingBarFill.color = spells[index].MyBarColor;
        spellName.text = spells[index].MyName;
        icon.sprite = spells[index].MyIcon;
        spellroutine = StartCoroutine(Progress(index));
        fadeRoutine = StartCoroutine(FadeBar());
        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;
        float rate = 1.0f / spells[index].MyCastTime;
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            castingBarFill.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            timePassed += Time.deltaTime;
            castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");

            if (spells[index].MyCastTime - timePassed < 0)
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
}
