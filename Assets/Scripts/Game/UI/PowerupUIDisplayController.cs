using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupUIDisplayController : SingletonMonobehaviour<PowerupUIDisplayController>
{
    public List<PowerupUIController> powerupUIList;

    private void Start()
    {
        powerupUIList = new List<PowerupUIController>();
    }

    private void Sprite(PowerupController ui)
    {
        Sprite sprite = ui.GetComponent<SpriteRenderer>().sprite;
    }

    private void AddUI(PowerupUIController powerupUI)
    {
        powerupUIList.Add(powerupUI);
    }

    private void OverrideUI(PowerupUIController powerupUI)
    {
        
    }

    private void RemoveUI(PowerupUIController powerupUI)
    {
        powerupUIList.Remove(powerupUI);
    }
}
