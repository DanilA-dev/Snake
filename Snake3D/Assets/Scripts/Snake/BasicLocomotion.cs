using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLocomotion : LocomotionState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SnakeState(animator).ChangeState(PlayerModState.Normal);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        SnakeMovement(animator).Movement();
   }

}
