using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public enum GameStateType : int
{
    PreStart = 0,
    Start = 1,
    Over = 2
}

public class GameState : MonoBehaviour
{

    [SerializeField] private List<GameStateEvent> gameStateEvent = new List<GameStateEvent>();

    private GameStateType currenState;

    private event Action<GameStateType> OnStateChanged;


    private void Start()
    {
        SwitchState(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void OnEnable()
    {
        OnStateChanged += GameState_OnStateChanged;
    }

    private void OnDestroy()
    {
        OnStateChanged -= GameState_OnStateChanged;
    }

    private void GameState_OnStateChanged(GameStateType type)
    {
        if(gameStateEvent.Count > 0)
        {
           for (int i = 0; i < gameStateEvent.Count; i++)
           {
                if(gameStateEvent[i].StateType == type)
                {
                    StartCoroutine(gameStateEvent[i].Invoke());
                }
           }
        }
    }


    public void SwitchState(int nextState)
    {
        currenState = (GameStateType)nextState;
        OnStateChanged?.Invoke(currenState);
    }

}


[Serializable]
public class GameStateEvent
{
    [SerializeField] private GameStateType stateType;
    [SerializeField] private UnityEvent OnStateEnter;
    [SerializeField] private float toInvoke;

    #region PROPERTIES
    
    public GameStateType StateType { get => stateType; }

    #endregion

    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(toInvoke);
        OnStateEnter?.Invoke();
        
    }
}