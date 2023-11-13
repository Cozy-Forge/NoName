using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum TestEnemyState
{

    Idle,
    Move,
    Attack,
    Die

}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TestEnemyController : StateController<TestEnemyState>
{

    [field:SerializeField] public EnemyDataSO Data { get; private set; }


    private class ExampleEnemyAttackState : EnemyState<TestEnemyState>
    {
        public ExampleEnemyAttackState(StateController<TestEnemyState> controller, EnemyDataSO data) : base(controller, data)
        {
        }

        protected override async void OnEnter()
        {

            Debug.Log("Attack");
            await Task.Delay((int)_data.AttackCoolDown * 1000);
            _controller.ChangeState(TestEnemyState.Move);

        }

        protected override void Run()
        {
        }

    }

    private void Awake()
    {
        
        Data = Instantiate(Data);

        var goMove = new EnemyTargetRangeTransition<TestEnemyState>
            (transform, Data.Range, Data.TargetAbleLayer, TestEnemyState.Move);

        var goAttack = new EnemyTargetRangeTransition<TestEnemyState>
            (transform, Data.AttackAbleRange, Data.TargetAbleLayer, TestEnemyState.Attack);

        var goIdleBase = new EnemyTargetRangeTransition<TestEnemyState>
            (transform, Data.Range, Data.TargetAbleLayer, TestEnemyState.Idle);

        var goIdle = new ReverseTransition<TestEnemyState>(goIdleBase);

        var idleState = new EnemyIdleState<TestEnemyState>(this, Data)
            .AddTransition(goMove);

        var moveState = new EnemyMoveState<TestEnemyState>(TestEnemyState.Idle, this, Data)
            .AddTransition(goAttack)
            .AddTransition(goIdle);

        var attackState = new ExampleEnemyAttackState(this, Data);

        _stateContainer.Add(TestEnemyState.Idle, idleState);
        _stateContainer.Add(TestEnemyState.Move, moveState);
        _stateContainer.Add(TestEnemyState.Attack, attackState);

        CurrentState = TestEnemyState.Idle;

    }

}
