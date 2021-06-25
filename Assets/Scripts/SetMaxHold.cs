using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMaxHold : MonoBehaviour
{
    [SerializeField]
    Text _maxHoldTime;
    float _displayedTime;

    private void OnEnable()
    {
        _displayedTime = PlayerPrefs.GetFloat("MaxHold", 0);
        _maxHoldTime.text = DisplayTime(_displayedTime);     
    }


    string DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.RoundToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void SetNewHold()
    {
        PlayerPrefs.SetFloat("MaxHold", _displayedTime);
        Timer.CheckForHold?.Invoke();
    }

    public void Revert()
    {
        _displayedTime = PlayerPrefs.GetFloat("MaxHold", 0);
        _maxHoldTime.text = DisplayTime(PlayerPrefs.GetFloat("MaxHold", 0));
        
    }

    public void AddMinutes()
    {
        _displayedTime += 60;
        _maxHoldTime.text = DisplayTime(_displayedTime);
    }

   public void SubtractMinutes()
    {
        _displayedTime -= 60;
        if (_displayedTime < 0) { _displayedTime = 0; }
        _maxHoldTime.text = DisplayTime(_displayedTime);
    }

   public void AddSeconds()
    {
        _displayedTime += 1;
        _maxHoldTime.text = DisplayTime(_displayedTime);
    }

   public void SubtractSeconds()
    {
        _displayedTime -= 1;
        if (_displayedTime < 0) { _displayedTime = 59; }
        _maxHoldTime.text = DisplayTime(_displayedTime);
    }
}
