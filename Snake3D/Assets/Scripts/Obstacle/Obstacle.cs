using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Obstacle : MonoBehaviour
{
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


    private void InvokeDamageEvent()
    {
        for (int i = 0; i < damageEvent.Count; i++)
        {
             StartCoroutine(damageEvent[i].Invoke());
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
    [SerializeField] private UnityEvent OnSnakeEnter;
    [SerializeField] private float toInvoke;


    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(toInvoke);
        OnSnakeEnter?.Invoke();
    }
}