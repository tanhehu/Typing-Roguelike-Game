using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupListController : SingletonMonobehaviour<PowerupListController>
{
    public List<PowerupController> powerupList;

    public PowerupController SpawnPowerup()
    {
        return CreateController.Instance.Create<PowerupController>(powerupList[Random.Range(0, powerupList.Capacity)]);         // Create a random powerup
    }
}
