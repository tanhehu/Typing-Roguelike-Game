using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    public Image image;
    public Text text;
    public AllCharacterController character;

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.wordOffset);   // This function translates the world position of the character
    }                                                                                                       // plus its offset (to push the word above the character) to a screen position
}                                                                                                           // which is then assigned to the word's position
                                                                                                            // making it follow the character on the screen