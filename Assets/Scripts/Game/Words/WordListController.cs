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
    public Dictionary<string, List<EnemyController>> wordDictionary = new Dictionary<string, List<EnemyController>>();
    public List<string> wordList = new List<string>();

    public Canvas wordCanvas;
    public Canvas powerupCanvas;

    public Buff buff = Buff.None;

    private void Awake()
    {
        foreach (var word in wordList)
        {
            wordDictionary.Add(word, new List<EnemyController>());
        }
    }

    private void DestroyEnemy(string word)                                  // Trigger all enemy bearing the word death and word destroy
    {
        while (wordDictionary[word].Count > 0)
        {
            var enemy = wordDictionary[word][0];
            enemy.deathDelegate?.Invoke();                    
            wordDictionary[word].Remove(enemy);
        }
    }

    public string RandomizeWord(EnemyController enemy)                       // Existing problem: when two enemies of same word appears, the word check only kills one
    {                                                                        // Solution: use list of enemies for a word
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
        wordDictionary[wordList[num]].Add(enemy);
        return wordList[num];
    }

public void WordCheck(string str)                                          // Design narrative: should there be multiple buff at once or just one (currently) 
    {
        if (wordDictionary.ContainsKey(str))                               // Check if the word is in the list  
        {
            DestroyEnemy(str);
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
                        DestroyEnemy(wordList[i]);
                        break;
                    }
                }
                break;

            case Buff.TrimLetterFirst:                                              // Or if trim first two letters is on effect
                for (int i = 0; i < wordList.Count; i++)
                {
                    if (wordList[i].Substring(2) == str)
                    {
                        DestroyEnemy(wordList[i]);
                        break;
                    }
                }
                break;

            case Buff.TrimLetterLast:                                                // Or if trim last two letters is on effect
                for (int i = 0; i < wordList.Count; i++)
                {
                    if (wordList[i].Substring(0, wordList[i].Length - 2) == str)
                    {
                        DestroyEnemy(wordList[i]);
                        break;
                    }
                }
                break;
        }
    }
}

