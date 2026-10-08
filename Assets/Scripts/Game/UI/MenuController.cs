using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnPause()
    {
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnDeath()
    {
        this.gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
