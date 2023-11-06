using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController<T> : MonoBehaviour where T : System.Enum
{

    private Dictionary<T, State<T>> _stateContainer = new();

    public T CurrentState { get; private set; } = default;

    protected virtual void Start()
    {
        
        foreach (var state in _stateContainer.Values)
        {

            state.Create();

        }

    }

    protected virtual void Update()
    {

        _stateContainer[CurrentState].Run();
        _stateContainer[CurrentState].ChackTransitioins();

    }

    public virtual void ChangeState(T state)
    {

        _stateContainer[CurrentState].OnExit();

        var old = CurrentState;

        CurrentState = state;
        _stateContainer[CurrentState].OnEnter();


    }

    protected virtual void OnDestroy()
    {

        foreach (var state in _stateContainer.Values)
        {

            state.Destroy();

        }

    }



}

public abstract class State<T> where T : System.Enum
{

    public State(StateController<T> controller)
    {

        _transform = controller.transform;
        _gameObject = controller.gameObject;
        _controller = controller;

    }

    protected HashSet<Transition<T>> _transitions = new();
    protected StateController<T> _controller;
    protected Transform _transform;
    protected GameObject _gameObject;

    public virtual void Create() { }
    public virtual void Destroy() { }
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public abstract void Run();

    public virtual void ChackTransitioins()
    {

        foreach(var itme in _transitions)
        {

            if (itme.ChackTransition())
            {

                _controller.ChangeState(itme.NextState);
                break;

            }

        }

    }

}

public abstract class Transition<T> where T : System.Enum
{

    public Transition(T nextState)
    {

        NextState = nextState;

    }

    public T NextState { get; private set; } = default;

    public abstract bool ChackTransition();

}