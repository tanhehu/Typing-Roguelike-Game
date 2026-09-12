using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : SingletonMonobehaviour<PlayerController>
{

}

public class PlayerController : AllCharacterController
{
    private bool disableInput = false;
    private float inputX;
    private float inputY;

    [Header("Health")]
    public float health = 100f;
    public Image healthImage;

    [SerializeField] private GameObject playerTypingField;
    private readonly int maxCharLength = 12;

    [Header("Game Over")]
    public Text gameOverScreen;
    public Button restartButton;

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
        if(!disableInput)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }
        Direction = new Vector3(inputX, inputY, 0);
        transform.position += speed * Direction * Time.deltaTime;
    }

    public override void Flip()
    {
        if((isFacingRight && inputX < 0) || (!isFacingRight && inputX > 0))                             // Change direction
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
        isWalking = inputX * inputX + inputY * inputY >= 0.25f;                                         // Walk if input is greater than a certain threshold
        this.isDying = health <= 0;
        base.Animation();
    }

    #endregion

    #region Typing Mechanics
    public void TypeWord()
    {
        foreach(var c in Input.inputString)
        {
            string str = word.text.text;
            if (c == '\b')                                                                              // Backspace
            {
                if(str != "")
                {
                    str = str.Remove(str.Length - 1);
                }
            }
            else if(c == '\r')                                                                          // Enter
            {
                WordList.Instance.WordCheck(str);                                                               // Check if the word is in the list
                str = "";                                                                               // Reset canvas after matching word
                word.text.text = str;
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

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerMask.Enemy)                                         // Check if collide with enemy
        {
            health -= collision.gameObject.GetComponent<EnemyController>().damage;                      // Retrieve enemy damage
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
        disableInput = true;
        inputX = 0;
        inputY = 0;
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
