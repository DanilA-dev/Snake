using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SnakeCollisions : MonoBehaviour
{
    [SerializeField] private SnakeColor snakeColor;
    [SerializeField] private List<CollectableCollisonEvent> eateableCollisions = new List<CollectableCollisonEvent>();
    [SerializeField] private ObstacleCollisionEvent obstacleCollision;


    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Obstacle"))
       {
            StartCoroutine(obstacleCollision.StartEvent());
       }

        ACollectable collectable = other.GetComponent<ACollectable>();
        if(collectable != null)
        {
            ICompareColor compareableColor = collectable.GetComponent<ICompareColor>();
            if(compareableColor != null)
            {
                switch(compareableColor.CheckColor(snakeColor.CurrentColorType))
                {
                    case true:
                        InvokeCollectableEvents(collectable.CollectableType);
                        break;
                    case false:
                        StartCoroutine(obstacleCollision.StartEvent());
                        break;
                }
            }
            InvokeCollectableEvents(collectable.CollectableType);
        }
    }

    private void InvokeCollectableEvents(CollectableType type)
    {
        for (int i = 0; i < eateableCollisions.Count; i++)
        {
            if(eateableCollisions[i].Collectable == type)
            {
               StartCoroutine(eateableCollisions[i].StartEvent());
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


    public IEnumerator StartEvent()
    {
           yield return new WaitForSeconds(toInvoke);
           OnCollision?.Invoke();
    }

}

[System.Serializable]
public class ObstacleCollisionEvent
{
    [SerializeField] private UnityEvent OnObstacleCollide;
    [SerializeField] private float toInvoke;

    public IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(toInvoke);
        OnObstacleCollide?.Invoke();
    }
}

