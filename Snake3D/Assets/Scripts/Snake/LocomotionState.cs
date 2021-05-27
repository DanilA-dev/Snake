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
        }
        return snakeMovement;
    }

    public SnakeState SnakeState(Animator animator)
    {
        if(snakeState == null)
        {
            snakeState = animator.GetComponentInParent<SnakeState>();

            if (snakeState == null)
            {
                snakeState = animator.GetComponent<SnakeState>();
            }
        }   
        return snakeState;
    }
}
