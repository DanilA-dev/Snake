using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public enum CollectableType
{
    Meal,Bonus
}

public enum CollectableState
{
    NotEaten, Eaten
}

public abstract class ACollectable : MonoBehaviour
{
    [SerializeField] private CollectableParams collectableParams;
    [SerializeField] private CollectablePosition collectablePosition;
    [SerializeField] private List<CollectableStateEvent> collectableStateEvent = new List<CollectableStateEvent>();

    protected abstract CollectableType Collectable { get; }
    protected CollectableState collectableState = CollectableState.NotEaten;

    private event Action<CollectableState> OnStateChanged;

    #region PROPERITES

    public CollectableParams CollectableParams { get => collectableParams; }
    public CollectablePosition CollectablePosition { get => collectablePosition; }

    public CollectableState CollectableState 
    {
        get => collectableState;
        set
        {
            collectableState = value;
            OnStateChanged?.Invoke(value);
        }
    }

    #endregion


    protected virtual void Awake()
    {
        OnStateChanged += ACollectable_OnStateChanged;
    }

    private void OnDestroy()
    {
        OnStateChanged -= ACollectable_OnStateChanged;
    }

    private void ACollectable_OnStateChanged(CollectableState state)
    {
        for (int i = 0; i < collectableStateEvent.Count; i++)
        {
            if(collectableStateEvent[i].CurrentState == state)
            {
                StartCoroutine(collectableStateEvent[i].StateEvent());
            }
        }
    }

    protected abstract void Activate();


    protected virtual IEnumerator Eat(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator InProcess(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
    }


}

[System.Serializable]
public class CollectableParams
{
    [SerializeField, Min(0)] private float toEatTime;
    [SerializeField, Min(0)] private float processingTime;
    [SerializeField, Min(0)] private float coolDownTime;

    #region PROPERTIES

    public float ToEatTime { get => toEatTime; }
    public float ProcessingTime { get => processingTime; }
    public float CoolDownTime { get => coolDownTime; }

    #endregion
} 

[System.Serializable]
public class CollectablePosition
{
    [SerializeField] private Vector3 minPos;
    [SerializeField] private Vector3 maxPos;

    private Vector3 RandomizePos()
    {
        var randomX = UnityEngine.Random.Range(minPos.x, maxPos.x);
        var randomZ = UnityEngine.Random.Range(minPos.z, maxPos.z);

        var newPos = new Vector3(randomX, 0.35f, randomZ);
        return newPos;
    }

    public void SetNewPosition(GameObject obj)
    {
        obj.transform.position = RandomizePos();
    }
    
}


[System.Serializable]
public class CollectableStateEvent
{
    [SerializeField] private CollectableState currentState;
    [SerializeField] private UnityEvent OnActiveState;
    [SerializeField] private float timeToInvoke;


    #region PROPERTIES

    public CollectableState CurrentState { get => currentState; }

    #endregion
    public IEnumerator StateEvent()
    {
        yield return new WaitForSeconds(timeToInvoke);
        OnActiveState?.Invoke();
    }

}