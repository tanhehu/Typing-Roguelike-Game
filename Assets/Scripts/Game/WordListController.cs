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
    public Dictionary<string, EnemyController> wordDictionaryNoCase = new Dictionary<string, EnemyController>();

    public List<string> wordList = new List<string>();


    public Canvas wordCanvas;

    public bool caseSensitiveBuff = false;

    private void Awake()
    {
        foreach(var word in wordList)
        {
            wordDictionary.Add(word, null);
            wordDictionaryNoCase.Add(word.ToLower(), null);
        }
    }

    public int RandomizeWord(EnemyController enemy)
    {
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

    public void BuffApplyCountDown(ref bool buff,float time)
    {
        StartCoroutine(BuffApllyCountDonwCoroutine(time));
    }

    private IEnumerator BuffApllyCountDonwCoroutine(float time)
    {
        caseSensitiveBuff = true;
        Debug.Log("Gained Removing Case Sensitive Buff");
        yield return new WaitForSeconds(time);
        caseSensitiveBuff = false;
        Debug.Log("Case Sensitive Buff Has Ended.");
    }

    public void WordCheck(string str)
    {

        if (wordDictionary.ContainsKey(str))                               // Check if the word is in the list  
        {
            wordDictionary[str].deathDelegate?.Invoke();                   // Trigger enemy death and word destroy
            wordDictionary[str] = null;
        }
        else if((caseSensitiveBuff && wordDictionaryNoCase.ContainsKey(str.ToLower())))      // or if the case sensitive buff is active and the word is in the list
        {
            wordDictionaryNoCase[str.ToLower()].deathDelegate?.Invoke();                    // Trigger enemy death and word destroy
            wordDictionaryNoCase[str.ToLower()] = null;
        }
    }
}

