using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateController : SingletonMonobehaviour<CreateController>
{
    public T Create<T>(T prefab) where T : MonoBehaviour
    {
        return Instantiate<T>(prefab);
    }
}
