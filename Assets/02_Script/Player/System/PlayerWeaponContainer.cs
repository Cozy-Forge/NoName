using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponContainer : MonoBehaviour
{

    [SerializeField] private Weapon debugWeapon;

    private HashSet<Weapon> _weapons = new();

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            AddWeapon(Instantiate(debugWeapon));

        }

    }

    public void AddWeapon(Weapon weapon)
    {

        _weapons.Add(weapon);
        var pos = Quaternion.AngleAxis(_weapons.Count * 30, Vector3.forward) * Vector2.right;

        weapon.transform.position = pos + transform.position;
        weapon.transform.SetParent(transform);

    }

    public void CastingAll(Transform target, float range)
    {

        foreach(var item in _weapons)
        {

            item.CastingWeapon(target, range);

        }

    }

}
