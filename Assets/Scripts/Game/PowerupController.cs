using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public PowerupEffectController powerupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        powerupEffect.ApplyEffect(Player.Instance);
        Destroy();
    }

    private void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}
