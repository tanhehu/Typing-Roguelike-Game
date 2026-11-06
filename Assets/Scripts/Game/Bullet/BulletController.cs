using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MoveController
{
    private float damage = 10;

    private void Start()
    {
        Destroy(this.gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == (int)LayerMask.Player)
        {
            Destroy();
            Player.Instance.OnHit(damage);
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
