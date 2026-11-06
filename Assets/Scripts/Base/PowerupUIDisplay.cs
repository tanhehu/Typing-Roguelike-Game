using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupUIDisplay : SingletonMonobehaviour<PowerupUIDisplay>
{
    public List<PowerupUI> powerupUIList;

    private void Start()
    {
        powerupUIList = new List<PowerupUI>();
    }

    private void Sprite(PowerupController ui)
    {
        Sprite sprite = ui.GetComponent<SpriteRenderer>().sprite;
    }

    private void AddUI(PowerupUI powerupUI)
    {
        powerupUIList.Add(powerupUI);
    }

    private void OverrideUI(PowerupUI powerupUI)
    {
        
    }

    private void RemoveUI(PowerupUI powerupUI)
    {
        powerupUIList.Remove(powerupUI);
    }

    public string Display(Buff type)
    {
        switch(type)
        {
            case Buff.RemoveCase:
                return "Allow removing case sensitive";
            case Buff.SlowTime:
                return "Time slow down";
            case Buff.TrimFirstTwo:
                return "Allow skipping first 2 letters";
            case Buff.TrimLastTwo:
                return "Allow skipping last 2 letters";
            case Buff.None:
                return "";
            default:
                return "";
        }
    }
}
