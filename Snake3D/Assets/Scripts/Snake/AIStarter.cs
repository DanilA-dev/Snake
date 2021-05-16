using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStarter : MonoBehaviour
{
    [SerializeField] private SnakeMovement snake;
    [SerializeField] private List<Transform> movePoints;

    private Vector3 target;
    private int pointsIndex;

    private void Start()
    {
        target = movePoints[0].position;
        pointsIndex = 0;
    }

    private void Update()
    {
        PointsMove();
    }

    private void PointsMove()
    {
        snake.Tails[0].Translate(snake.Tails[0].forward * Time.smoothDeltaTime/10f, Space.World);
        snake.Tails[0].LookAt(target);

        if (Vector3.Distance(snake.Tails[0].position, target) <= 0.3f)
        {
            ChangePoint();
        }
    }

    private void ChangePoint()
    {
        if (pointsIndex == movePoints.Count - 1)
        {
            pointsIndex = -1;
        }

        pointsIndex++;
        target = movePoints[pointsIndex].position;
    }

}
