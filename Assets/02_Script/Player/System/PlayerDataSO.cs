using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player/Data")]
public class PlayerDataSO : ScriptableObject
{

    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    [field: SerializeField] public LayerMask DashObstacleLayer { get; private set; }
    [field: SerializeField] public float DashLength { get; private set; }
    [field: SerializeField] public float DashPower { get; private set; }

    [SerializeField] private float _dashCoolDown;

    public bool IsCoolDown { get; private set; }

    public void SetCoolDown()
    {

        if (IsCoolDown) return;

        IsCoolDown = true;

        FAED.InvokeDelay(() =>
        {

            IsCoolDown = false;

        }, _dashCoolDown);

    }

}
