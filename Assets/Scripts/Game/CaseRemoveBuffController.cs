//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu (menuName = "Powerups/CaseRemoveBuff")]
//public class CaseRemoveBuffController : PowerupEffectController
//{
//    public float time;
//    public override void ApplyEffect()
//    {
//        foreach(var word in WordList.Instance.wordList)
//        {
//            foreach(char c in word)
//            {
//                if((int)c < 97 || (int)c > 122)
//                {
//                    c = char.ToUpper(c);
//                }
//            }
//        }    

//        foreach(var word in WordList.Instance.wordList)
//        {
//            Debug.Log(word);
//        }
//        base.ApplyEffect();
//    }
//}
