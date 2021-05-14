using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SnakeCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"I hit {other.name}");
    }
}
