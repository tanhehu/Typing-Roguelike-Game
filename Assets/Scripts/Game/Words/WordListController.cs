using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WordList : SingletonMonobehaviour<WordListController>
{

}

public class WordListController : MonoBehaviour
{
    public Dictionary<string, EnemyController> wordDictionary = new Dictionary<string, EnemyController>();
    public List<string> wordList = new List<string>();


    public Canvas wordCanvas;

    public bool caseSensitiveBuff = false;
    public int trimLetterBuff = 0;

    private void Awake()
    {
        foreach (var word in wordList)
        {
            wordDictionary.Add(word, null);
        }
    }

    private void EnemyDestroyInvoke(string check)
    {
        wordDictionary[check].deathDelegate?.Invoke();                    // Trigger enemy death and word destroy
        wordDictionary[check] = null;
    }

    public int RandomizeWord(EnemyController enemy)                       // Existing problem: when two enemies of same word appears, the word check only kills one
    {                                                                     // Solution: use list of enemies for a word
        bool checkNull = false;
        int checkTime = 0;
        int num = 0;
        while(!checkNull && checkTime < 100)                                                                 
        {                                                                                  
            num = Random.Range(0, wordList.Count);
            if (wordDictionary[wordList[num]] == null)
            {
                wordDictionary[wordList[num]] = enemy;
                checkNull = true;
            }
            checkTime++;
        }
        return num;
    }

    public void WordCheck(string str)
    {
        if (wordDictionary.ContainsKey(str))                               // Check if the word is in the list  
        {
            EnemyDestroyInvoke(str);
        }
        else if(caseSensitiveBuff)                                         // or if the case sensitive buff is active and the word is in the list
        {
            string check = "";
            for(int i = 0; i < wordList.Count; i++)
            {
                if (wordList[i].ToLower() == str.ToLower())
                {
                    check = wordList[i];
                    break;
                }
            }
            if(check != "")
            {
                EnemyDestroyInvoke(check);
            }
        }
        else if(trimLetterBuff != 0)
        {
            string check = "";
            if (trimLetterBuff == 1)
            {
                for (int i = 0; i < wordList.Count; i++)
                {
                    if (wordList[i].Substring(2) == str)
                    {
                        check = wordList[i];
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < wordList.Count; i++)
                { 
                    if (wordList[i].Substring(0, wordList[i].Length - 2) == str)
                    {
                        check = wordList[i];
                        break;
                    }
                }
            }
            
            if(check != "")
            {
                EnemyDestroyInvoke(check);
            }
        }
    }
}

