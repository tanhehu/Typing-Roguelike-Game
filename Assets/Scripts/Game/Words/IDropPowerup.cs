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
    Ork = 30,
    BloodMonster = 25,
    Demon = 40,
    LootBox = 70
}