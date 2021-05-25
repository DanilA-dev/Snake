using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Crystal : ACollectable
{

    public override CollectableType CollectableType => CollectableType.Crystal;
    public static bool isFeverModeOn = false;

    private void OnEnable()
    {
        SnakeCollisions.OnFeverModeActivate += SnakeCollisions_OnFeverModeActivate;
    }

    private void SnakeCollisions_OnFeverModeActivate()
    {
        StopAllCoroutines();
        StartCoroutine(InProcess(CollectableParams.ProcessingTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        SnakeCollisions snake = other.GetComponent<SnakeCollisions>();
        if(snake != null)
        {
            Activate();
        }
    }

    

    protected override void Activate()
    {
        if(CollectableState == CollectableState.NotEaten)
        {
            StartCoroutine(Eat(CollectableParams.ToEatTime));
        }
    }

    protected override IEnumerator Eat(float time)
    {
        CollectableState = CollectableState.Eaten;

        if(CollectableState == CollectableState.Eaten)
        {
            yield return new WaitForSeconds(time);

            CollectableState = CollectableState.NotEaten;
            Animate();
        }

    }


    protected override IEnumerator InProcess(float time)
    {
        CollectableState = CollectableState.InProcess;
        if(CollectableState == CollectableState.InProcess)
        {
            isFeverModeOn = true;
            yield return new WaitForSeconds(time);
            isFeverModeOn = false;
            CollectableState = CollectableState.NotEaten;
        }
    }


    private void Animate()
    {
        for (int i = 0; i < animations.Count; i++)
        {
            animations[i].Animate();
        }
    }

}
