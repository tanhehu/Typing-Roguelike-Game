using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    GameScene = 1,
    SettingScene = 2
}

public class TransitionButton : MonoBehaviour, IButton
{
    public Scene scene;

    public TransitionButton(Scene scene) { this.scene = scene; }

    public void OnClick()
    {
        SceneManager.LoadScene((int)scene);
    }
}
