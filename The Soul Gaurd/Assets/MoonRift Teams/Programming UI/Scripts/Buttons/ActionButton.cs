using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    private IUseable useable;

    public IUseable MyUseable { get; set; }

    public Button MyButton { get; private set; }
    public Image MyIcon { get => icon; set => icon = value; }

    [SerializeField]
    private Image icon;

    private void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (MyUseable != null)
        {
            MyUseable.Use();
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
