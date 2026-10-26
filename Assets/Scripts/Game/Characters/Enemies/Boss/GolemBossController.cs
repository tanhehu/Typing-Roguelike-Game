using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossController : EnemyBase
{
    public override void Start()
    {
        base.Start();
        wordOffset = new Vector3(0, 2, 0);
    }

    public override void OnDeath()
    {
        StartCoroutine(OnWinCoroutine());
    }

    private IEnumerator OnWinCoroutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        MenuController.Instance.OnWin();
    }
}
