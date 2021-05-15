using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : ACollectable
{
    protected override CollectableType Collectable { get => CollectableType.Meal; }

    private ScoreSystem score;

    private void Start()
    {
        CollectablePosition.SetNewPosition(this.gameObject);
        score = ScoreSystem.Instance;
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
        StartCoroutine(Eat(CollectableParams.ToEatTime));
    }


    protected override IEnumerator Eat(float time)
    {
        yield return new WaitForSeconds(time);
        CollectablePosition.SetNewPosition(this.gameObject);
        score.AddScore(CollectableParams.ScoreForCollect);

    }

    

}
