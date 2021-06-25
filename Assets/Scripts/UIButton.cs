using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : EventTrigger
{
    Button _button;

    private void Start()
    {
        _button = transform.GetComponent<Button>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        AudioManager.Instance.PlayEffect(0, 1);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        if (_button.interactable)
        {
            AudioManager.Instance.PlayEffect(1, 1);
        }
        else
        {
            AudioManager.Instance.PlayEffect(2, 1);
        }

    }
}
