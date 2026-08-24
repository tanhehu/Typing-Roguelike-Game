using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class WordListController : MonoBehaviour
{
    public Dictionary<string, GameObject> wordDictionary = new Dictionary<string, GameObject>();
    public List<string> wordList = new List<string> { "Jack", "Tan", "Matthew" };

    public Canvas wordCanvas;

    private void Awake()
    {
        foreach(var word in wordList)
        {
            wordDictionary.Add(word, null);
        }
    }

    public void Update()
    {
        
    }
}

public class WordList : SingletonMonobehaviour<WordListController>
{

}

