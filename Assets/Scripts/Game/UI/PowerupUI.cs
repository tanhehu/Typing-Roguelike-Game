using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    public Image powerupImage;
    public Text powerupDuration;
    public Text description;
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
        description.text = PowerupUIDisplay.Instance.Display(powerup.powerupEffect.type);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
