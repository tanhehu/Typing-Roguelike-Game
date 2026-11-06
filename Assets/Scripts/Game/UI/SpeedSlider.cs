using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedData
{
    public float speed;
}

public class SpeedSlider : SingletonMonobehaviour<SpeedSlider>
{
    private Slider slider;
    public float baseSpeed = 3;
    public float speedRange = 3;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void OnValueChange()
    {
        var data = new SpeedData { speed = baseSpeed + speedRange * slider.value };
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Speed", json);
        PlayerPrefs.Save();
    }
}
