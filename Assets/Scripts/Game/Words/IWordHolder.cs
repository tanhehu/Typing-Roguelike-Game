using System;
using UnityEngine;
using UnityEngine.Events;

public interface IWordHolder : IDamageable
{
    public WordController word { get; set; }
    public Vector3 wordOffset { get; set; }

    public Action destroy { get; set; }

    GameObject GameObject();

    void SpawnWord();
}
