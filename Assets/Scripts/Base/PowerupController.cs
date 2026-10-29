using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public PowerupEffectBase powerupEffect;
    public PowerupUI powerupUIPrb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerupUI powerupUI = CreateController.Instance.Create<PowerupUI>(powerupUIPrb);
        powerupUI.SetAttributes(this);
        StartCoroutine(EffectCountDown(powerupEffect.duration));
    }

    private IEnumerator EffectCountDown(float time)
    {
        powerupEffect.ApplyEffect(Player.Instance);
        DisableObject();
        yield return new WaitForSecondsRealtime(time);
        powerupEffect.RemoveEffect(Player.Instance);
        Destroy();
    }

    private void DisableObject()
    {
        this.transform.position = new Vector3(999, 999, 0);
    }

    private void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}
