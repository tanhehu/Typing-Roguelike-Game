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
        character.deathDelegate += Destroy;
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.wordOffset);
    }

    public void Destroy()
    {
        character.deathDelegate -= Destroy;
        character = null;
        this.gameObject.SetActive(false);
    }
}