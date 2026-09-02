using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : AllCharacterController
{
    private float inputX;
    private float inputY;

    [Header("Health")]
    public float health = 100f;
    [SerializeField] private Image healthImage;

    [SerializeField] private GameObject playerTypingField;
    private readonly int maxCharLength = 12;

    [Header("Game Over")]
    public Text gameOverScreen;
    public Button restartButton;
    public Sprite deathSprite;

    public override void Start()
    {
        base.Start();
        word.character = this;
    }

    public override void Update()
    {
        base.Update();
        TypeWord();
    }

    #region Base

    public override void Move()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        Direction = new Vector3(inputX, inputY, 0);
        base.Move(Direction);
    }

    public override void Flip()
    {
        if((isFacingRight && inputX < 0) || (!isFacingRight && inputX > 0))
        {
            base.Flip();
        }
    }

    public override void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            base.Attack();
        }
    }

    public override void Animation()
    {
        isWalking = inputX * inputX + inputY * inputY >= 0.25f;
        this.isDying = health <= 0;
        base.Animation();
    }

    #endregion

    public void TypeWord()
    {
        foreach(var c in Input.inputString)
        {
            string str = word.text.text;
            if (c == '\b')
            {
                if(str != "")
                {
                    str = str.Remove(str.Length - 1);
                }
            }
            else if(c == '\r')
            {
                if (WordList.Instance.wordDictionary.ContainsKey(str))
                {
<<<<<<< HEAD
                    WordList.Instance.wordDictionary[str].GetComponent<EnemyController>().onDeath?.Invoke();
=======
                    WordList.Instance.wordDictionary[str].deathDelegate?.Invoke();                      // Trigger enemy death and word destroy
>>>>>>> fefa3974b6001809cf68855b6b0fa0f2d4037efe
                    WordList.Instance.wordDictionary[str] = null;
                    str = "";
                    word.text.text = str;
                }
                break;
            }
            else if(c == ' ' || (int)c < 65 || (int)c > 122 || ((int)c > 90 && (int)c < 97))            // Reference to ASII Table, check if the char is anything beside letters
            {
                continue;
            }
            else
            {
                str += c.ToString();
            }

            if(str.Length > maxCharLength)                                                              // Max letters allowed
            {
                str = str.Remove(str.Length - 1, 1);
            }
            word.text.text = str;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerMask.Enemy)
        {
            health -= collision.gameObject.GetComponent<EnemyController>().damage;
            healthImage.fillAmount = health / 100f;
            if(health <= 0)
            {
                gameOverScreen.gameObject.SetActive(true);
                animator.Play("PlayerDeath");
                StartCoroutine(QuitGame());
            }
        }
    }

    private IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}

public class Player : SingletonMonobehaviour<PlayerController>
{

}
