using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Idle, Move, Laser

public abstract class GlowyState : State<EnumGlowyState>
{
    protected GlowyState(StateController<EnumGlowyState> controller, LaserMonsterDataSO data) : base(controller)
    {
        _data = data;
    }

    protected LaserMonsterDataSO _data;
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

public class GlowyLaserState : GlowyState
{
    public GlowyLaserState(StateController<EnumGlowyState> controller, LaserMonsterDataSO data, 
                        LineRenderer lineRenderer) : base(controller, data)
    {
        _lineRenderer = lineRenderer;
    }

    private LineRenderer _lineRenderer;

    protected override void OnEnter()
    {
        SetTarget(_data.AttackAbleRange);
        if(_target == null)
        {
            _controller.ChangeState(EnumGlowyState.Idle);
            return;
        }    

        Debug.Log("Laser");
        _controller.AddCoroutine(LaserShot());
        
    }

    protected override void Run()
    {
        
    }

    private IEnumerator LaserShot()
    {
        _lineRenderer.enabled = true;

        float curTime = 0f;
        Vector3 dir = Vector3.zero;
        while (curTime <= _data.LaserTargetingTime)
        {
            float toPlayerDist = Vector2.Distance(_target.position, _transform.position);
            dir = (_target.position - _transform.position).normalized;
            Vector3 hitPos = ShotRay(toPlayerDist, dir, true);

            LineRendererSetPos(hitPos, 0.01f, Color.red);

            curTime += Time.deltaTime;
            yield return null;
        }

        LineRendererSetPos(ShotRay(_data.LaserRange, dir), 0.01f, Color.red);
        yield return new WaitForSeconds(_data.LaserDelay);

        curTime = 0f;
        while (curTime <= _data.LaserHoldingTime)
        {
            Vector3 hitPos = ShotRay(_data.LaserRange, dir);

            DamageCheck(dir, _data.LaserRange, _data.TargetAbleLayer);
            LineRendererSetPos(hitPos, Mathf.Lerp(0.1f, 0.5f, curTime / _data.LaserHoldingTime), Color.white);

            curTime += Time.deltaTime;
            yield return null;
        }

        _lineRenderer.enabled = false;

        _data.SetCoolDown();
        _controller.ChangeState(EnumGlowyState.Idle);
    }

    private void DamageCheck(Vector3 dir, float dist, LayerMask targetLayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, dir, dist, targetLayer);
        if (hit.collider)
        {
            //damage
            float damage = Mathf.Clamp(_data.LaserDamage, 1, int.MaxValue);

            if (hit.collider.TryGetComponent<HPObject>(out var hp))
            {
                hp.TakeDamage(damage);
            }
            else if (hit.collider.TryGetComponent<Hitbox>(out var box))
            {
                box.Casting(damage);
            }
        }
    }

    private Vector3 ShotRay(float dist, Vector3 dir, bool toTargetCheck = false)
    {
        dir.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(_transform.position, dir, dist, _data.WallLayer);
        if(hit.collider) //
        {
            
            return hit.point;
        }

        if (toTargetCheck) // if target check
            return _target.position;

        Vector3 pos = _transform.position + dir * dist;
        return pos;
    }

    private void LineRendererSetPos(Vector3 pos, float width, Color color)
    {
        _lineRenderer.SetPosition(0, _transform.position);
        _lineRenderer.SetPosition(1, pos);

        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;

        _lineRenderer.startWidth = width;
        _lineRenderer.endWidth = width;
    }
}
