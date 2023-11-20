using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Idle, Move, Laser

public abstract class GlowyState : State<EnumGlowyState>
{
    protected GlowyState(StateController<EnumGlowyState> controller, GlowyDataSO data) : base(controller)
    {
        _data = data;
    }

    protected GlowyDataSO _data;
    protected Transform _target;

    protected void SetTarget(float length)
    {

        var hit = Physics2D.OverlapCircle(_transform.position, length, _data.TargetAbleLayer);

        if (hit != null)
        {

            _target = hit.transform;

        }
        else
        {

            _target = null;

        }

    }

}

public class GlowyMoveState : GlowyState
{
    public GlowyMoveState(StateController<EnumGlowyState> controller, GlowyDataSO data) : base(controller, data)
    {
    }

    protected override void Run()
    {
        
    }
}

public class GlowyLaserState : GlowyState
{
    public GlowyLaserState(StateController<EnumGlowyState> controller, GlowyDataSO data) : base(controller, data)
    {
    }

    protected override void Run()
    {
        
    }
}
