using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public abstract class GolemBossState
{
    protected GolemBossController golem;
    protected Animator animator;
    protected Vector3 playerPos => Player.Instance.transform.position;
    protected float distance => Vector3.Distance(playerPos, golem.transform.position);
    protected float timer = 0;

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
        if (distance < golem.range)
        {
            golem.ChangeState(new GolemBossMeleeState(golem));
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                int random = UnityEngine.Random.Range(0, 2);

                if (random == 0 && !golem.hasSpawn)
                {
                    golem.SpawnEnemy(UnityEngine.Random.Range(2, 6));
                    golem.hasSpawn = true;
                }
                else golem.ChangeState(new GolemBossRangedState(golem));

                timer = 0;
            }

            if(golem.waveCount <= 0)
            {
                golem.ChangeState(new GolemBossWeakState(golem));
                golem.hasSpawn = false;
                golem.waveCount = 100000;
            }
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
        animator.SetBool("Melee", true);
    }

    public override void Update()
    {
        if(distance > golem.range)
        {
            golem.ChangeState(new GolemBossIdleWalkState(golem));
        }
    }

    public override void Exit()
    {
        animator.SetBool("Melee", false);
    }
}

public class GolemBossRangedState : GolemBossState
{
    public GolemBossRangedState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {
        animator.SetBool("Ranged", true);
    }

    public override void Exit()
    {
        animator.SetBool("Ranged", false);
    }
}

public class GolemBossWeakState : GolemBossState
{
    public GolemBossWeakState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {
        animator.SetBool("Weak", true);
        golem.word.EnableWord();
    }

    public override void Exit()
    {
        animator.SetBool("Weak", false);

        golem.health--;
        golem.word.Destroy();
    }
}

public class GolemBossDeathState : GolemBossState
{
    public GolemBossDeathState(GolemBossController golem) : base(golem) { }

    public override void Enter()
    {
        animator.SetBool("Death", true);
    }
}