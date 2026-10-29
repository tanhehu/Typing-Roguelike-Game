using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : SingletonMonobehaviour<PlayerController>
{

}

public class PlayerController : AllCharacterBase
{
    public bool disableInput = false;
    private float inputX;
    private float inputY;
    private bool goX;
    private bool goY;
    private bool stop = false;
    private float signX;
    private float signY;

    [Header("Health")]
    public float health = 100f;
    public Image healthImage;

    [SerializeField] private GameObject playerTypingField;
    private readonly int maxCharLength = 12;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        TypeWord();
        MenuHandler();
    }

    #region Base

    public override void Move()
    {
        transform.rotation = Quaternion.identity;
        if(!disableInput)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
            if (inputX != 0) { signX = Mathf.Sign(inputX); stop = false; }
            if (inputY != 0) { signY = Mathf.Sign(inputY); stop = false; }

            if(!stop)
            {   
                goX = inputX != 0 || !goY;
                goY = inputY != 0 || !goX;
            }

            if (Input.GetKeyDown(KeyCode.Space)) { goX = false; goY = false; stop = true; }

            if (goX) inputX = 1 * signX;
            if (goY) inputY = 1 * signY;
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
        base.Animation();
    }

    #endregion

    #region Interfaces

    public override void OnHit(float damage)
    {
        health -= damage;
        healthImage.fillAmount = health / 100f;
        if (health <= 0)
        {
            destroy?.Invoke();
        }
    }

    public override void OnDeath()
    {
        disableInput = true;
        inputX = 0;
        inputY = 0;
        animator.Play("PlayerDeath");
        StartCoroutine(MenuInvokeOnDeath());
    }

    #endregion

    #region Typing Mechanics
    public void TypeWord()
    {
        if (disableInput) return;

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
                WordList.Instance.WordCheck(str);                                                       // Check if the word is in the list
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

    public void MenuHandler()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            MenuController.Instance.OnPause();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerMask.Enemy)                                         // Check if collide with enemy
        {
            OnHit(collision.gameObject.GetComponent<EnemyBase>().damage);                      // Retrieve enemy damage
        }
    }

    private IEnumerator MenuInvokeOnDeath()
    {
        yield return new WaitForSeconds(1);
        MenuController.Instance.OnDeath();
    }
}
