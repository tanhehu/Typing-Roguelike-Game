using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : AllCharacterController
{
    [SerializeField] private float range;
    [SerializeField] private Image wordPrefab; 

    private Vector3 playerPos => Player.Instance.transform.position;
    private Vector3 distance => playerPos - transform.position;

    private void Start()
    {
        SpawnWord();
    }

    public override void Update()
    {
        Direction = distance;
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Flip()
    {
        if((distance.x > 0 && !isFacingRight) || (distance.x <= 0 && isFacingRight))
        {
            base.Flip();
        }
    }

    public void SpawnWord()
    {
        Image image = CreateController.Instance.Create<Image>(wordPrefab);
        foreach(var word in WordList.Instance.wordDictionary)
        {
            if(word.Value)
            {
                var text = image.transform.GetChild(0);
                text.gameObject.GetComponent<Text>().text = word.Key;
                WordList.Instance.wordDictionary[word.Key] = false;
                break;
            }
        }
        Debug.Log("Text");
    }

    public void DrawRange()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnDrawGizmos()
    {
        DrawRange();
    }
}
