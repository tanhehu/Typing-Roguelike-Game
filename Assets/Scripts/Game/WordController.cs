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
<<<<<<< HEAD
        character.onDeath += Destroy;
    }

    private void OnDisable()
    {
        character.onDeath -= Destroy;
=======
        character.deathDelegate += Destroy;
>>>>>>> fefa3974b6001809cf68855b6b0fa0f2d4037efe
    }

    private void Update()
    {
<<<<<<< HEAD
        transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.wordOffset);  
    }

    private void Destroy()
    {
        Destroy(this);
    }                                                                                                    
}                                                                                                           
                                                                                                            
=======
        transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.wordOffset);
    }

    public void Destroy()
    {
        character.deathDelegate -= Destroy;
        character = null;
        this.gameObject.SetActive(false);
    }
}
>>>>>>> fefa3974b6001809cf68855b6b0fa0f2d4037efe
