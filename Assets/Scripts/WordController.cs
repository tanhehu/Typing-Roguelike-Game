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
        character.onDeath += Destroy;
    }

    private void OnDisable()
    {
        character.onDeath -= Destroy;
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.wordOffset);  
    }

    private void Destroy()
    {
        Destroy(this);
    }                                                                                                    
}                                                                                                           
                                                                                                            