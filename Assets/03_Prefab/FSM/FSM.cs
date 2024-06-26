using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubState
{

    public void Run();

}

public delegate void StateChangeEvent<T>(T oldState, T newState) where T : System.Enum;

public class StateController<T> : MonoBehaviour where T : System.Enum
{

    protected Dictionary<T, State<T>> _stateContainer = new();
    protected HPObject _hpObject;

    public event StateChangeEvent<T> OnStateChangeEvent;

    public T CurrentState { get; protected set; } = default;

    protected virtual void Start()
    {
        
        _hpObject = GetComponent<HPObject>();

        if( _hpObject != null)
        {

            _hpObject.OnDieEvent += HandleDieEvent; 

        }

        foreach (var state in _stateContainer.Values)
        {

            state.Create(); 

        }

        ChangeState(CurrentState);

    }

    protected virtual void HandleDieEvent()
    {

        enabled = false;

    }

    protected virtual void Update()
    {

        _stateContainer[CurrentState].Execute();
        _stateContainer[CurrentState].ChackTransitioins();

    }

    public virtual void ChangeState(T state)
    {

        _stateContainer[CurrentState].Exit();

        var old = CurrentState;

        CurrentState = state;
        _stateContainer[CurrentState].Enter();

        OnStateChangeEvent?.Invoke(old, state);

    }

    protected virtual void OnDestroy()
    {

        foreach (var state in _stateContainer.Values)
        {

            state.Destroy();

        }

    }

    public Coroutine AddCoroutine(IEnumerator coroutine)
    {

        return StartCoroutine(coroutine);

    }

    public void RemoveCoroutine(Coroutine coroutine)
    {

        StopCoroutine(coroutine);

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
    protected HashSet<ISubState> _subStates = new();
    protected StateController<T> _controller;
    protected Transform _transform;
    protected GameObject _gameObject;
    protected bool _isControlReleased;

    public virtual void Create() { }
    public virtual void Destroy() { }

    public void Enter()
    {

        _isControlReleased = false;
        OnEnter();

    }

    public void Exit()
    {

        _isControlReleased = true;
        OnExit();

    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }

    public void Execute()
    {

        Run();

        if (_isControlReleased) return;

        RunSubState();

    }

    protected abstract void Run();

    private void RunSubState()
    {

        foreach (var item in _subStates)
        {

            item.Run();

        }

    }

    public virtual void ChackTransitioins()
    {

        foreach(var item in _transitions)
        {

            if (item.ChackTransition())
            {

                _controller.ChangeState(item.NextState);
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

public class BoolTransition<T> : Transition<T> where T : System.Enum
{

    public bool ChackValue;

    protected BoolTransition(T nextState) : base(nextState)
    {
    }

    public override bool ChackTransition()
    {

        return ChackValue;

    }
}

public class ReverseTransition<T> : Transition<T> where T : System.Enum
{

    public ReverseTransition(Transition<T> transition) : base(transition.NextState)
    {

        _transition = transition;

    }

    private Transition<T> _transition;

    public override bool ChackTransition()
    {

        return !_transition.ChackTransition();

    }

} 