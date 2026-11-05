using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossController : EnemyBase
{
    public GolemBossState currentState;

    public override void Start()
    {
        base.Start();
        ChangeState(new GolemBossSpawnState(this));
        wordOffset = new Vector3(0, 2, 0);
    }

    public override void Update()
    {
        Move();
        Flip();

        currentState?.Update();
    }

    public void ChangeState(GolemBossState next)
    {
        currentState?.Exit();
        currentState = next;
        currentState?.Enter();
    }

    public void CheckAnimEnd()
    {
        ChangeState(new GolemBossIdleWalkState(this));
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
