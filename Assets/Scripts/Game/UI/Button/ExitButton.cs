using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour, IButton
{
    public void OnClick()
    {
        Application.Quit();
    }
}
