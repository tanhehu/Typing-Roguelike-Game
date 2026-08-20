using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class WordListController : MonoBehaviour
{
    public Dictionary<string, bool> wordDictionary = new Dictionary<string, bool>();
    public List<string> wordList = new List<string> { "Jack", "Tan", "Matthew" };

    private void Awake()
    {
        foreach(var word in wordList)
        {
            wordDictionary.Add(word, true);
        }
    }

    public void Update()
    {
        
    }
}

public class WordList : SingletonMonobehaviour<WordListController>
{

}

