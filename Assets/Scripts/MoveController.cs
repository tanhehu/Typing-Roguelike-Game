using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed;
    private Vector3 direction;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    private void Update()
    {
        Move(direction);
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
