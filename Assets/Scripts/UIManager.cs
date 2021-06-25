using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    Timer _timer;
    [SerializeField]
    GameObject _mainMenu;
    [SerializeField]
    GameObject _settings,_changeMaxHold;
    [SerializeField]
    Button _CO2Button,_O2Button;



    private void Awake()
    {
        Timer.CheckForHold += HoldCheck;
        HoldCheck();
    }

    private void Start()
    {
        MainMenu();
    }


    private void HoldCheck()
    {
        if (PlayerPrefs.GetFloat("MaxHold", 0) <= 0)
        {
            _CO2Button.interactable = false;
            _O2Button.interactable = false;
        }
        else
        {
            _CO2Button.interactable = true;
            _O2Button.interactable = true;
        }
    }

    public void CO2Table()
    {
        _timer.gameObject.SetActive(true);
        _timer.SetTimerMode(TableType.CO2);
        _mainMenu.SetActive(false);
    }

    public void O2Table()
    {

        _timer.gameObject.SetActive(true);
        _timer.SetTimerMode(TableType.O2);
        _mainMenu.SetActive(false);
    }

    public void MaxHold()
    {
        _timer.gameObject.SetActive(true);
        _timer.SetTimerMode(TableType.MAXTEST);
        _mainMenu.SetActive(false);
    }

    public void Settings()
    {
        _mainMenu.SetActive(false);
        _settings.SetActive(true);
    }

    public void MainMenu()
    {
        _mainMenu.SetActive(true);
        _timer.gameObject.SetActive(false);
        _settings.gameObject.SetActive(false);
        _changeMaxHold.SetActive(false);
    }

    public void ChangeMaxHoldMenu()
    {
        _settings.SetActive(false);
        _changeMaxHold.SetActive(true);
    }
    public void ResetMaxHold()
    {
        PlayerPrefs.SetFloat("MaxHold", 0);
        Timer.CheckForHold?.Invoke();
    }
}
