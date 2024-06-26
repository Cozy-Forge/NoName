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

    private Dictionary<float, HandleBulletController> _bulletHandleController = new();


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
                    Speed = item.Key

                }.Schedule(item.Value.BulletContainer);

            }

        }

    }

    public void AddBullet(Bullet bullet)
    {

        if (!_bulletHandleController.ContainsKey(bullet.Data.Speed))
        {

            _bulletHandleController.Add(bullet.Data.Speed, new HandleBulletController());

        }

        _bulletHandleController[bullet.Data.Speed].HandleJob.Complete();

        _bulletHandleController[bullet.Data.Speed].BulletContainer.Add(bullet.transform);

    }

    public bool RemoveBullet(Bullet bullet)
    {

        if (!_bulletHandleController.ContainsKey(bullet.Data.Speed)) return true;

        _bulletHandleController[bullet.Data.Speed].HandleJob.Complete();


        for (int i = 0; i < _bulletHandleController[bullet.Data.Speed].BulletContainer.length; i++)
        {

            if (_bulletHandleController[bullet.Data.Speed].BulletContainer[i] == bullet.transform)
            {

                _bulletHandleController[bullet.Data.Speed].BulletContainer.RemoveAtSwapBack(i);
                return true;

            }

        }

        //JobSystem이 프레임 관련해서 무언가 문제가 있음 자세히는 파악 못함
        //일반 리스트와 NativeArray간 비교 차이 발생
        //NativeContainer문제일 가능성?
        //지금은 안되는거 확인 했지만 나중에 그 원인이 확실했는지 파악해서 고칠것
        Debug.Log("BulletNotRemoved");
        Debug.Log(_bulletHandleController[bullet.Data.Speed].BulletContainer.length);
        return false;

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
