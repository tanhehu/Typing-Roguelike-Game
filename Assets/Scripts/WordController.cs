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

    private void OnDisable()
    {
        character.deathDelegate -= Destroy;
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.wordOffset);  
    }

    public void Destroy()
    {
        this.gameObject.SetActive(false);
        character.deathDelegate -= Destroy;
        character = null;
    }
}                                                                                                                                                                                                         
