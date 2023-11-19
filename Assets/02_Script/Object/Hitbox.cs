using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{


    private HPObject _hp;

    private void Awake()
    {
        
        _hp = transform.root.GetComponent<HPObject>();

    }

    public void Casting(float damage)
    {

        _hp.TakeDamage(damage);

    }

}
