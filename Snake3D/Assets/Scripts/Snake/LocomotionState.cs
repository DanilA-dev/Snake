using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocomotionState : StateMachineBehaviour
{
    private SnakeMovement snakeMovement;
    private SnakeState snakeState;

    public SnakeMovement SnakeMovement(Animator animator)
    {
        if(snakeMovement == null)
        {
           snakeMovement = animator.GetComponent<SnakeMovement>();
            if(snakeMovement == null)
            {
                Debug.LogError("snakeMovement is null");
            }
        }
        return snakeMovement;
    }

    public SnakeState SnakeState(Animator animator)
    {
        if(snakeState == null)
        {
            snakeState = animator.GetComponent<SnakeState>();
            if(snakeState == null)
            {
                Debug.LogError("snakeState is null");
            }
        }  
        return snakeState;
    }
}
