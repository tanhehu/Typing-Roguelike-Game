using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : TransitionButton
{
    public PlayButton(Scene scene) : base(scene) { }

    private void Awake()
    {
        this.scene = Scene.GameScene;
    }
}
