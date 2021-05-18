using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : ACollectable
{

    private void Start()
    {
        CollectablePosition.SetNewPosition(this.gameObject);
        IdleAnimation();
    }


    private void IdleAnimation()
    {
        for (int i = 0; i < animations.Count; i++)
        {
            animations[i].Animate();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        SnakeCollisions snake = other.GetComponent<SnakeCollisions>();
        if (snake != null)
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
            CollectablePosition.SetNewPosition(this.gameObject);
            CollectableState = CollectableState.NotEaten;
        }
    }

    

}
