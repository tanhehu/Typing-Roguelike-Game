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

public enum Buff
{
    None,
    RemoveCase,
    TrimLetterFirst,
    TrimLetterLast
}

public class WordListController : MonoBehaviour
{
    public Dictionary<string, List<IWordHolder>> wordDictionary = new Dictionary<string, List<IWordHolder>>();
    public List<string> wordList = new List<string>();

    public Canvas wordCanvas;
    public Canvas powerupCanvas;

    public WordController wordPrb;

    public Buff buff = Buff.None;

    private void Awake()
    {
        foreach (var word in wordList)
        {
            wordDictionary.Add(word, new List<IWordHolder>());
        }
    }

    private void DestroyTarget(string word)                                  // Trigger all enemy bearing the word death and word destroy
    {
        while (wordDictionary[word].Count > 0)
        {
            var enemy = wordDictionary[word][0];
            enemy.OnHit(0);                    
            wordDictionary[word].Remove(enemy);
        }
    }

    public string RandomizeWord(IWordHolder target)                    
    {                                                                       
        bool checkNull = false;
        int checkTime = 0;
        int num = 0;
        while (!checkNull && checkTime < 100)
        {
            num = Random.Range(0, wordList.Count);
            if (wordDictionary[wordList[num]].Count == 0)
            {
                checkNull = true;
            }
            checkTime++;
        }
        wordDictionary[wordList[num]].Add(target);
        return wordList[num];
    }

public void WordCheck(string str)                                          // Design narrative: should there be multiple buff at once or just one (currently) 
    {
        if (wordDictionary.ContainsKey(str))                               // Check if the word is in the list  
        {
            DestroyTarget(str);
            return;
        }

        switch (buff)
        {
            case Buff.None:
                break;
            case Buff.RemoveCase:                                                  // Or if case remove is on effect
                for (int i = 0; i < wordList.Count; i++)
                {
                    if (wordList[i].ToLower() == str.ToLower())
                    {
                        DestroyTarget(wordList[i]);
                        break;
                    }
                }
                break;

            case Buff.TrimLetterFirst:                                              // Or if trim first two letters is on effect
                for (int i = 0; i < wordList.Count; i++)
                {
                    if (wordList[i].Substring(2) == str)
                    {
                        DestroyTarget(wordList[i]);
                        break;
                    }
                }
                break;

            case Buff.TrimLetterLast:                                                // Or if trim last two letters is on effect
                for (int i = 0; i < wordList.Count; i++)
                {
                    if (wordList[i].Substring(0, wordList[i].Length - 2) == str)
                    {
                        DestroyTarget(wordList[i]);
                        break;
                    }
                }
                break;
        }
    }
}

