using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] private SnakeMovement snake;
    [SerializeField] private GameObject tailPrefab;

    [Range(0.2f, 5)]
    [SerializeField] private float tailDistance;
    [Range(1, 20)]
    [SerializeField] private int startSize;


    private void Start()
    {
        SetTailSize();
    }

    private void Update()
    {
        TailMovement(snake.Tails);
    }

    public void TailMovement(List<Transform> tails)
    {
        for (int i = 1; i < tails.Count; i++)
        {
            Transform currentTail = tails[i];
            Transform prevTail = tails[i - 1];
            Vector3 newPos = prevTail.position;

            float distance = Vector3.Distance(prevTail.position, currentTail.position);
            float tailSpeed = Time.deltaTime * distance / tailDistance * snake.SnakeSpeed;

            if (tailSpeed > 0.5f)
            {
                tailSpeed = 0.5f;
            }

            currentTail.position = Vector3.Slerp(currentTail.position, newPos, tailSpeed);
            currentTail.rotation = Quaternion.Slerp(currentTail.rotation, prevTail.rotation, tailSpeed);
        }
    }

    private void SetTailSize()
    {
        if (snake.Tails.Count > 0)
        {
            for (int i = 0; i < startSize - 1; i++)
            {
                AddBody();
            }
        }
        else
        {
            throw new Exception("Tails list is empty");
        }
    }

    public void AddBody()
    {
       var newTail = Instantiate(tailPrefab, snake.Tails[snake.Tails.Count - 1].position, snake.Tails[snake.Tails.Count - 1].rotation, transform);
        snake.Tails.Add(newTail.transform);
    }
}
