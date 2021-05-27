using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;



public class SnakeCollisions : MonoBehaviour
{
    [SerializeField] private Animator snakeAnim;
    [SerializeField] private SnakeColor snakeColor;
    [SerializeField] private List<CollectableCollisonEvent> eateableCollisions = new List<CollectableCollisonEvent>();
    [SerializeField] private ObstacleCollisionEvent obstacleCollision;

    private List<ACollectable> crystals = new List<ACollectable>();


    private bool isModActivate = false;
    private float timeBetween = 0.5f;
    private float currTime;


    private void OnTriggerEnter(Collider other)
    {
        CollectableCheck(other);
        ObstacleCheck(other);
    }


    // v drugoy klass
    private IEnumerator FeverModCheck()
    {
        if(crystals.Count > 0)
        {
             for (float i = 0; i < timeBetween; i+= Time.deltaTime)
             {
                 currTime = i;
                 if(!isModActivate)
                 {
                    if(crystals.Count >= 3)
                    {
                        snakeAnim.SetBool("isFeverModOn", true);
                    }
                    currTime = 0f;
                 }
                 yield return new WaitForEndOfFrame();
             }

            isModActivate = false;
            crystals.Clear();
        }
    }

    private void CollectableCheck(Collider other)
    {
        ACollectable collectable = other.GetComponent<ACollectable>();
        if (collectable != null)
        {
            if (collectable is Crystal)
            {
                crystals.Add(collectable);

                StopAllCoroutines();
                StartCoroutine(FeverModCheck());
            }
            HumanColorCheck(collectable);
        }
    }

    private void ObstacleCheck(Collider other)
    {
        Obstacle obs = other.GetComponent<Obstacle>();
        if (obs != null)
        {
            obs.DamageSnake();

            StartCoroutine(obstacleCollision.StartEvent(snakeAnim.GetBool("isFeverModOn")));
        }
    }

    private void HumanColorCheck(ACollectable collectable)
    {
        ICompareColor compareableColor = collectable.GetComponent<ICompareColor>();
        if (compareableColor != null)
        {
            switch (compareableColor.CheckColor(snakeColor.CurrentColorType))
            {
                case true:
                    InvokeCollectableEvents(collectable.CollectableType);
                    break;
                case false:
                    StartCoroutine(obstacleCollision.StartEvent(snakeAnim.GetBool("isFeverModOn")));
                    break;
            }
        }
        else
        {
            InvokeCollectableEvents(collectable.CollectableType);
        }
    }

    private void InvokeCollectableEvents(CollectableType type)
    {
        for (int i = 0; i < eateableCollisions.Count; i++)
        {
            if(eateableCollisions[i].Collectable == type)
            {
               StartCoroutine(eateableCollisions[i].StartEvent(this));
            }
        }
    }
}

[System.Serializable]
public class CollectableCollisonEvent
{
    [SerializeField] private CollectableType collectable;
    [SerializeField] private float toInvoke;
    [SerializeField] private UnityEvent OnCollision;

    #region PROPERITES

    public CollectableType Collectable { get => collectable; }

    #endregion


    public IEnumerator StartEvent(MonoBehaviour monoBeh)
    {
        yield return new WaitForSeconds(toInvoke);
        OnCollision?.Invoke();

        if(collectable == CollectableType.Crystal)
        {
            monoBeh.StartCoroutine(FeverModCheck());
        }
    }

    private IEnumerator FeverModCheck()
    {
        yield return new WaitForSeconds(0);
    }

}

[System.Serializable]
public class ObstacleCollisionEvent
{
    [SerializeField] private UnityEvent OnObstacleCollide;
    [SerializeField] private float toInvoke;

    public IEnumerator StartEvent(bool canDamage)
    {
        if(!canDamage)
        {
           yield return new WaitForSeconds(toInvoke);
           OnObstacleCollide?.Invoke();
        }
    }
}

