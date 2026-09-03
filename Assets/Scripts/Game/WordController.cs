using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    public Image image;
    public Text text;
    public AllCharacterController character;

    private void Start()
    {
        character.deathDelegate += Destroy;                     // Add destroy function to event trigger
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.wordOffset);       // Set and translate canvas to Screen (UI) position that follow the target + offset
    }

    public void Destroy()
    {
        this.gameObject.SetActive(false);
        character.deathDelegate -= Destroy;                     // Remove destroy function to event trigger
        character = null;
    }
}
