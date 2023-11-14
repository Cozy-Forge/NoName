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
        weapon.OnEquip();

    }

    public void CastingAll(Collider2D[] arr)
    {

        foreach(var item in _weapons)
        {

            var targetData = FirstObj(arr, item.transform);

            item.CastingWeapon(targetData.target, targetData.range);

        }

    }

    private (Transform target, float range) FirstObj(Collider2D[] arr, Transform root)
    {

        Transform trm = null;
        float minRange = float.MaxValue;

        foreach (var item in arr)
        {

            float dist = Vector2.Distance(root.position, item.transform.position);

            if (dist < minRange)
            {

                trm = item.transform;
                minRange = dist;

            }

        }

        return (trm, minRange);

    }

}
