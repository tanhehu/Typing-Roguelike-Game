using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class WordListController : MonoBehaviour
{
    public Dictionary<string, EnemyController> wordDictionary = new Dictionary<string, EnemyController>();
    public List<string> wordList = new List<string>();

    public Canvas wordCanvas;

    private void Awake()
    {
        foreach(var word in wordList)
        {
            wordDictionary.Add(word, null);
        }
    }

    public int ChooseWord(EnemyController enemy)
    {
        bool checkNull = false;
        int num = 0;
        while(!checkNull)
        {
            num = Random.Range(0, wordList.Capacity);
            if (wordDictionary[wordList[num]] == null)
            {
                wordDictionary[wordList[num]] = enemy;
                checkNull = true;
            }
        }
        return num;
    }
}

public class WordList : SingletonMonobehaviour<WordListController>
{

}

