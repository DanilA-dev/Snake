using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public enum PlayerModState
{
    Normal,
    Fever

}
public class SnakeState : MonoBehaviour
{
    [SerializeField] private List<ModStateEvent> modState = new List<ModStateEvent>();

    private PlayerModState playerModState = PlayerModState.Normal;
    private event Action<PlayerModState> OnModStateChanged;

    #region PROPERTIES

    public PlayerModState PlayerModState
    {
        get => playerModState;
        set
        {
            playerModState = value;
            OnModStateChanged?.Invoke(value);
        }
        
    }

    #endregion


    private void OnEnable()
    {
        OnModStateChanged += SnakeState_OnModStateChanged;
    }

    private void OnDisable()
    {
        OnModStateChanged -= SnakeState_OnModStateChanged;
    }

    private void SnakeState_OnModStateChanged(PlayerModState state)
    {
        foreach (var m in modState)
        {
            if(m.CurrentModState == state)
            {
                m.Invoke();
            }
        }
    }

    public void ChangeState(PlayerModState nextState)
    {
        playerModState = nextState;
    }

}
[Serializable]
public class ModStateEvent
{
    [SerializeField] private PlayerModState currentModState;
    [SerializeField] private UnityEvent OnStateEnter;
    [SerializeField] private float toInvoke;

    #region PROPERTIES

    public PlayerModState CurrentModState { get => currentModState; }

    #endregion


    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(toInvoke);
        OnStateEnter?.Invoke();
    }
}