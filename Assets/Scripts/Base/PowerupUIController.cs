using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUIController : MonoBehaviour
{
    public Image powerupImage;
    public Text powerupDuration;
    private float count;

    void Update()
    {
        powerupDuration.text = ((int)(count -= Time.unscaledDeltaTime)).ToString();
        if (int.Parse(powerupDuration.text) <= 0)
        {
            Destroy();
        }
    }

    public void SetAttributes(PowerupController powerup)
    {
        transform.SetParent(WordList.Instance.wordCanvas.transform, false);
        powerupImage.sprite = powerup.GetComponent<SpriteRenderer>().sprite;
        count = powerup.powerupEffect.duration;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
