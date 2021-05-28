using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFeverMod : LocomotionState
{
    [SerializeField] private string boolString;
    [SerializeField] private float speedModifier;
    [SerializeField] private float modTime;

    private float feverSnakeSpeed;
    private float startTime;

    private Vector3 StartPos(Animator a)
    {
        Vector3 startPos = new Vector3(0f, SnakeMovement(a).Tails[0].position.y, SnakeMovement(a).Tails[0].position.z);
        return startPos;
    }


   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {

        SnakeState(animator).PlayerModState = PlayerModState.Fever;
        startTime = modTime;
        feverSnakeSpeed = SnakeMovement(animator).SnakeSpeed * speedModifier;
   }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CameraShake.Instance.Shake(modTime, 0.001f);
        SetSnakeToPosition(animator);
    }


    private void SetSnakeToPosition(Animator a)
    {
        SnakeMovement(a).Tails[0].position = Vector3.MoveTowards(SnakeMovement(a).Tails[0].position,
                                      StartPos(a), Time.deltaTime * SnakeMovement(a).SnakeSpeed);

        if(SnakeMovement(a).Tails[0].position == StartPos(a))
        {
            SnakeMovement(a).Tails[0].Translate(Vector3.forward * Time.deltaTime * feverSnakeSpeed, Space.World);
            ModTick(a);
        }
    }

    private void ModTick (Animator a)
    {
        if(a != null)
        {
            if(startTime <= 0)
            {
                a.SetBool(boolString, false);
                startTime = modTime;
            }
            else
            {
              startTime -= Time.deltaTime;
            }
        }
    }
}
