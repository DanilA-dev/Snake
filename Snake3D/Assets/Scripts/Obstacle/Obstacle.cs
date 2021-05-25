using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ObstacleState
{
    Damaging,
    NotDamaging
}

public class Obstacle : MonoBehaviour
{
    private ObstacleState obstacleState = ObstacleState.Damaging;
    [SerializeField] private List<ObstacleDamageEvent> damageEvent = new List<ObstacleDamageEvent>();
    [SerializeField] private List<SimpleTweenAnimation> animations = new List<SimpleTweenAnimation>();

    private void Start()
    {
        Animate();
    }

    public void DamageSnake()
    {
        InvokeDamageEvent();
    }

    private void ChangeState()
    {
        obstacleState = ObstacleState.NotDamaging;
    }


    private void InvokeDamageEvent()
    {
        for (int i = 0; i < damageEvent.Count; i++)
        {
            if(damageEvent[i].State == obstacleState)
            {
                StartCoroutine(damageEvent[i].Invoke());
            }
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
[System.Serializable]
public class ObstacleDamageEvent
{
    [SerializeField] private ObstacleState state;
    [SerializeField] private UnityEvent OnSnakeEnter;
    [SerializeField] private float toInvoke;

    #region PROPERTIES

    public ObstacleState State { get => state; }

    #endregion

    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(toInvoke);
        OnSnakeEnter?.Invoke();
    }
}