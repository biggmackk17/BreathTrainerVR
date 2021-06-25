using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TableGenerator
{

    [SerializeField]
    static public Cycle[] CreateTable(TableType _tableType)
    {
        var _maxBreathHold = PlayerPrefs.GetFloat("MaxHold", 0);
        Cycle[] _table = new Cycle[16];
        if (_tableType == TableType.CO2)
        {

            for (int i = 0; i < _table.Length; i++)
            {
                if (i % 2 == 0)
                {

                    if (i == 0)
                    {
                        _table[i] = new Cycle(ActionType.BREATHE, 120);
                    }
                    else
                    {
                        _table[i] = new Cycle(ActionType.BREATHE, 120 - ((i / 2)+ -1) * 15);
                    }
              
                    

                }
                else
                {
                    _table[i] = new Cycle(ActionType.HOLD, _maxBreathHold * .6f);

                }

            }

        }
        else if(_tableType == TableType.O2)
        {

            for (int i = 0; i < _table.Length; i++)
            {
                if (i % 2 != 0)
                {
                   var percentile = (_table.Length-(i)) / 2 * .025f;
                   var maxPercent = .85f;

                    var timeCalc = _maxBreathHold * (maxPercent - percentile);


                    _table[i] = new Cycle(ActionType.HOLD, timeCalc);

                }
                else
                {
                    _table[i] = new Cycle(ActionType.BREATHE, 120);

                }

            }

        }
        for (int i = 0; i < _table.Length; i++)
        {
            Debug.Log("Type: " + _table[i]._action+ " Length: " +  _table[i]._time);
        }
        return _table;
    }
}


public class Cycle
{

   public ActionType _action;
   public float _time;
   public Cycle(ActionType action, float time)
    {
        _action = action;
        _time = time;

    }
}
    public enum TableType
    {
      O2,
      CO2,
      MAXTEST
    }
    public enum ActionType
    {
        BREATHE,
        HOLD
    }