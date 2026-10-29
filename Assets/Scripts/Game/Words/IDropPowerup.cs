using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropPowerup
{
    int dropChance { get; set; }
    bool drop { get; set; }
}

public enum DropChance
{
    Ork = 20,
    BloodMonster = 15,
    Demon = 30,
    LootBox = 50
}