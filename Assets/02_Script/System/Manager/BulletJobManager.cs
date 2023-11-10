using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
public struct BulletJob : IJobParallelForTransform
{

    public float Dt;
    public float Speed;

    public void Execute(int index, TransformAccess transform)
    {

        var dir = GetRight(transform.rotation);

        float3 pos = transform.position;

        transform.position = pos + (dir * Dt * Speed);

    }

    private float3 GetRight(in Quaternion rotation)
    {

        return rotation * Vector3.right;

    }

}

public class HandleBulletController : IDisposable
{

    public TransformAccessArray BulletContainer;
    public JobHandle HandleJob;

    public HandleBulletController()
    {

        BulletContainer = new TransformAccessArray(0);

    }

    public void Dispose()
    {

        BulletContainer.Dispose();

    }

}

public class BulletJobManager : MonoBehaviour
{
    
    public static BulletJobManager Instance { get; private set; }

    private Dictionary<BulletData, HandleBulletController> _bulletHandleController = new();

    private void Awake()
    {
        
        Instance = this;

    }

    private void Update()
    {

        foreach(var item in _bulletHandleController)
        {

            if (item.Value.HandleJob.IsCompleted)
            {

                item.Value.HandleJob = new BulletJob
                {

                    Dt = Time.deltaTime,
                    Speed = item.Key.Speed

                }.Schedule(item.Value.BulletContainer);

            }

        }

    }

    public void AddBullet(Bullet bullet)
    {

        if (!_bulletHandleController.ContainsKey(bullet.Data))
        {

            _bulletHandleController.Add(bullet.Data, new HandleBulletController());

        }

        _bulletHandleController[bullet.Data].BulletContainer.Add(bullet.transform);

    }

    public void RemoveBullet(Bullet bullet)
    {

        if (!_bulletHandleController.ContainsKey(bullet.Data)) return;

        for(int i = 0; i < _bulletHandleController[bullet.Data].BulletContainer.length; i++)
        {

            if (_bulletHandleController[bullet.Data].BulletContainer[i] == bullet.transform)
            {

                _bulletHandleController[bullet.Data].BulletContainer.RemoveAtSwapBack(i);
                break;

            }

        }

    }

    private void OnDestroy()
    {

        Instance = null;

        foreach(var item in _bulletHandleController.Values)
        {

            item.Dispose();

        }

    }

}