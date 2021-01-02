using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons;

    //private KeyCode action1, action2, action3;

    [SerializeField]
    private GameObject targetFrame;

    private Stat healthStat;

    [SerializeField]
    private Image portraitFrame;

    [SerializeField]
    private CanvasGroup keyBindMenu;

    private GameObject[] keybindButtons;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    private void Start()
    {
        //keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        healthStat = targetFrame.GetComponentInChildren<Stat>();

        //action1 = KeyCode.Alpha1;
        //action2 = KeyCode.Alpha2;
        //action3 = KeyCode.Alpha3;
        SetUseable(actionButtons[0], SpellBook.MyInstance.GetSpell("Red"));//Blue//Green
        SetUseable(actionButtons[1], SpellBook.MyInstance.GetSpell("Blue"));
        SetUseable(actionButtons[2], SpellBook.MyInstance.GetSpell("Green"));
    }

    private void Update()
    {
        //if (Input.GetKeyDown(action1))
        //{
        //    //ActionButtonOnClick(0);
        //}
        //if (Input.GetKeyDown(action2))
        //{
        //    //ActionButtonOnClick(1);
        //}
        //if (Input.GetKeyDown(action3))
        //{
        //    //ActionButtonOnClick(2);
        //}
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OpenCloseMenu();
        }
    }

    //private void ActionButtonOnClick(int buttonIndex)
    //{
    //    actionButtons[buttonIndex].onClick.Invoke();
    //}

    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);
        healthStat.Initialized(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);
        portraitFrame.sprite = target.MyPortrait;
        target.healthChanged += new HealthChanged(UpdateTargetFrame);
        target.characterRemoved += new CharacterRemoved(HideTargetFrame);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    public void OpenCloseMenu()
    {
        keyBindMenu.alpha = keyBindMenu.alpha > 0 ? 0 : 1;
        keyBindMenu.blocksRaycasts = keyBindMenu.blocksRaycasts == true ? false : true;
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }

    public void UpdateKetText(string key, KeyCode code)
    {
        TextMeshProUGUI tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void SetUseable(ActionButton button, IUseable useable)
    {
        button.MyIcon.sprite = useable.MyIcon;
        button.MyIcon.color = Color.white;
        button.MyUseable = useable;
    }
}
