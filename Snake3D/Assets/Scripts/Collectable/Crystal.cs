using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Crystal : ACollectable
{
    public override CollectableType CollectableType => CollectableType.Crystal;
    

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


    private void Animate()
    {
        for (int i = 0; i < animations.Count; i++)
        {
            animations[i].Animate();
        }
    }

}
