using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public PowerupEffectController powerupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        powerupEffect.ApplyEffect(Player.Instance);
        StartCoroutine(EffectCountDown(powerupEffect.duration));
    }

    private IEnumerator EffectCountDown(float time)
    {
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
