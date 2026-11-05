using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GolemBossState
{
    protected GolemBossController golem;
    protected Animator animator;
    protected float distance => Vector3.Distance(Player.Instance.transform.position, golem.transform.position);

    public GolemBossState(GolemBossController golem) 
    { 
        this.golem = golem; 
        this.animator = golem.Animator; 
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

public class GolemBossSpawnState : GolemBossState
{
    public GolemBossSpawnState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {
        animator.SetBool("Spawn", true);
    }

    public override void Exit()
    {
        animator.SetBool("Spawn", false);
    }
}

public class GolemBossIdleWalkState : GolemBossState
{
    public GolemBossIdleWalkState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {
        animator.SetBool("Idle", true);
    }

    public override void Update()
    {
        if(distance < golem.Range)
        {
            golem.ChangeState(new GolemBossMeleeState(golem));
        }
    }

    public override void Exit()
    {
        animator.SetBool("Idle", false);
    }
}

public class GolemBossMeleeState : GolemBossState
{
    public GolemBossMeleeState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {
        animator.SetFloat("Range", distance);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}

public class GolemBossWeakState : GolemBossState
{
    public GolemBossWeakState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}

public class GolemBossDeathState : GolemBossState
{
    public GolemBossDeathState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}