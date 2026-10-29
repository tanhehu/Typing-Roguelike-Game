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
}
