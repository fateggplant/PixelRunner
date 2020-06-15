using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 position = transform.position;
        position.x = target.position.x + 5;
        transform.position = position;
    }
}
