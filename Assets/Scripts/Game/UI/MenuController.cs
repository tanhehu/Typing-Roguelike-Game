using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : SingletonMonobehaviour<MenuController>
{
    private void Awake()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        Player.Instance.disableInput = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;
        Player.Instance.disableInput = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OnRestart()
    {
        Player.Instance.disableInput = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OnDeath()
    {
        Player.Instance.disableInput = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public void OnWin()
    {
        Player.Instance.disableInput = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
