using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{

    public static Action CheckForHold;
  
    WaitForSecondsRealtime _wait;
    [SerializeField]
    GameObject _mainMenu;
    [Header("Text Fields")]
    [SerializeField]
    Text _timerText;
    [SerializeField]
    Text _ActionText;
    [SerializeField]
    Text _cycleText;
    [SerializeField]
    Text _maxHoldText;

    [Header("Buttons")]
    [SerializeField]
    GameObject _startButton;
    [SerializeField]
    GameObject _stopButton;
    [SerializeField]
    GameObject _skipButton;
    [SerializeField]
    GameObject _exitButton;

    [SerializeField]
    GameObject _greatJob;


    float _time;
    bool _countUp;
    [SerializeField]
    TableType _timerMode;

    // Start is called before the first frame update
    void Start()
    {
        _wait = new WaitForSecondsRealtime(1);
   
    }
    private void OnEnable()
    {
        DisplayTime(0);
        SetTimerMode(TableType.CO2);
        _startButton.SetActive(true);
        _stopButton.SetActive(false);
        _skipButton.SetActive(false);
        _greatJob.SetActive(false);

    }

    public void SetTimerMode(TableType timerMode)
    {
        _timerMode = timerMode;
        if (_timerMode == TableType.MAXTEST)
        {
            _cycleText.gameObject.SetActive(false);
            _ActionText.gameObject.SetActive(false);
            _maxHoldText.gameObject.SetActive(true);
            _exitButton.SetActive(true);
            UpdateMaxHold();
            
        }
        else
        {
            _cycleText.gameObject.SetActive(true);
            _ActionText.gameObject.SetActive(true);
            _maxHoldText.gameObject.SetActive(false);
            _exitButton.SetActive(false);
        }

      
    }

    private void UpdateMaxHold()
    {
        var max = PlayerPrefs.GetFloat("MaxHold", 0);

        float minutes = Mathf.FloorToInt(max / 60);
        float seconds = Mathf.RoundToInt(max % 60);

        var maxText = string.Format("{0:00}:{1:00}", minutes, seconds);

        _maxHoldText.text = "Personal Best: " + maxText;
        CheckForHold?.Invoke();


    }

    public void StartTimer()
    {
        switch(_timerMode)
        {
            case TableType.CO2:
                StartCoroutine(CountDown(TableGenerator.CreateTable(TableType.CO2)));
              
                break;
            case TableType.O2: StartCoroutine(CountDown(TableGenerator.CreateTable(TableType.O2)));
             
                break;
            case TableType.MAXTEST: StartCoroutine(CountUp());
                _cycleText.gameObject.SetActive(false);
                _ActionText.gameObject.SetActive(false);

                
                break;

        }
        _startButton.SetActive(false);
        _stopButton.SetActive(true);
      
    }

    public void StopButton()
    {
        if (_timerMode == TableType.MAXTEST)
        {
            StopCountUp();
   
            
        }
        else
        {
            MainMenu();
           
        }
    }

    public void StopCountUp()
    {
        _countUp = false;
        _stopButton.SetActive(false);
        _startButton.SetActive(true);
        _exitButton.SetActive(true);
    }

    public void MainMenu()
    {
        _mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }


    public void SkipCycle()
    {
        _time = 0;
    }



    IEnumerator CountUp()
    {
        _countUp = true;
        _time = 0;
        while (_countUp)
        {
            yield return _wait;
            _time += 1;
            DisplayTime(_time);

        }

        if (_time > PlayerPrefs.GetFloat("MaxHold", 0))
        {
            PlayerPrefs.SetFloat("MaxHold", _time);
            UpdateMaxHold();
        }

        var test = PlayerPrefs.GetFloat("MaxHold", 0);
        Debug.Log(test);

    }


    IEnumerator CountDown(Cycle[] table)
    {

        for (int i = 0; i < table.Length; i++)
        {
            _time = table[i]._time;
            DisplayTime(_time);

            _cycleText.text = (i +1) +" / "+ table.Length;
            if (table[i]._action == ActionType.HOLD)
            {
                _ActionText.text = "Hold";
            }
            else
            {
                _ActionText.text = "Breathe";
            }

    
            while (_time > 0)
            {
                if (_time <= 10 && _time != 0)
                {
                    AudioManager.Instance.PlayEffect(3, 1);
                }
                else if (_time == 0)
                {
                    //play cycle finish sound
                }
            
                yield return new WaitForSecondsRealtime(1f);
                _time -= 1;
                DisplayTime(_time);

            }
            AudioManager.Instance.PlayEffect(4, 1);
         
        }
        Debug.Log("finished");
        AudioManager.Instance.PlayEffect(6, 1);
        _greatJob.SetActive(true);

        //play workout finished sound, show text that says GREAT JOB! 

    }


    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.RoundToInt(timeToDisplay % 60);

        _timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);

        

    }
}
