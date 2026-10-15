using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    MenuScene = 0,
    TutorialScene = 1,
    GameScene = 2,
    SettingScene = 3
}

public class TransitionButton<T> : MonoBehaviour, IButton where T : MonoBehaviour, IButton
{
    public Scene scene;

    public TransitionButton(Scene scene) { this.scene = scene; }

    public virtual void OnClick()
    {
        SceneManager.LoadScene((int)scene);
        Time.timeScale = 1;
    }
}
