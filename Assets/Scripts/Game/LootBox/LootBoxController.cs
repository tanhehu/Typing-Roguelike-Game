using System;
using System.Collections;
using UnityEngine;

public class LootBoxController : MonoBehaviour, IWordHolder, IDropPowerup
{
    private Animator animator;
    public WordController word { get; set; }
    public Vector3 wordOffset { get; set; } = new Vector3(0, 1, 0);
    public Action destroy { get; set; }

    public int dropChance { get; set; }
    public bool drop { get; set; }

    private void OnEnable()
    {
        destroy += Destroy;
    }

    private void OnDisable()
    {
        destroy -= Destroy;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SpawnWord();
        drop = UnityEngine.Random.Range(1, 100) <= (int)DropChance.LootBox;
    }

    #region Interfaces

    public void OnHit(float damage = 0)
    {
        animator.Play("OrkDeath");
        destroy?.Invoke();
    }

    public GameObject GameObject() { return this.gameObject; }

    #endregion

    private void Destroy()
    {
        StartCoroutine(OnDestroyCoroutine());
    }

    private IEnumerator OnDestroyCoroutine()
    {
        yield return new WaitForSeconds(1);
        PowerupController powerup;
        if (drop)
        {
            powerup = PowerupListController.Instance.SpawnPowerup();                            // Spawn powerup
            powerup.transform.position = this.transform.position;                               // Set powerup position to enemy death position
        }
        gameObject.SetActive(false);
    }


    public void SpawnWord()
    {
        word = CreateController.Instance.Create<WordController>(WordList.Instance.wordPrb);
        word.transform.SetParent(WordList.Instance.wordCanvas.transform, false);                 // Set word to a separate canvas (word canvas) so the word can appear on game screen
        word.text.text = WordList.Instance.RandomizeWord(this);                                 // Choose a word from the word pool using its index   
        word.holder = this;
    }
}
