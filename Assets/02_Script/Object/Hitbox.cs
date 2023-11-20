using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{


    private HPObject _hp;
    [Header("Monster HPObject Transform")]
    [SerializeField]
    private HPObject _rootHP;

    private void Awake()
    {
        if (_rootHP == null)
            _hp = transform.root.GetComponent<HPObject>();
        else
            _hp = _rootHP;
    }

    public void Casting(float damage)
    {

        _hp.TakeDamage(damage);

    }

}
