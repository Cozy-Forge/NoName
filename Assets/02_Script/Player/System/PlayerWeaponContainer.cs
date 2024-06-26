using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponContainer : MonoBehaviour
{

    [SerializeField] private Weapon debugWeapon;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private int _maxWeapon = 6;

    private HashSet<Weapon> _weapons = new();

    private void Awake()
    {

        if(debugWeapon != null)
        {

            AddWeapon(Instantiate(debugWeapon));

        }

    }

    private void Update()
    {

        if (weapons.Count < 0) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            AddWeapon(Instantiate(weapons[0]));

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            AddWeapon(Instantiate(weapons[1]));

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            AddWeapon(Instantiate(weapons[2]));

        }


    }

    public void AddWeapon(Weapon weapon)
    {

        if (_weapons.Count >= _maxWeapon)
        {

            var ls = _weapons.ToList();
            _weapons.Add(weapon);
            weapon.transform.SetParent(transform);
            _maxWeapon++;

            for (int i = 0; i < ls.Count; i++)
            {

                ls[i].transform.position = Quaternion.AngleAxis(360 / _maxWeapon * i, Vector3.forward) * Vector2.right + transform.position;

            }


        }
        else
        {


            _weapons.Add(weapon);
            var pos = Quaternion.AngleAxis(360 / _maxWeapon * _weapons.Count, Vector3.forward) * Vector2.right;

            weapon.transform.position = pos + transform.position;
            weapon.transform.SetParent(transform);
            weapon.OnEquip();

        }


    }

    public void RemoveWeapon(Weapon weapon)
    {

        if (_weapons.Count <= 0) return;

        _weapons.Remove(weapon);

        Destroy(weapon.gameObject);

        var ls = _weapons.ToList();
        _maxWeapon = ls.Count;

        for (int i = 0; i < ls.Count; i++)
        {

            ls[i].transform.position = Quaternion.AngleAxis(360 / _maxWeapon * i, Vector3.forward) * Vector2.right + transform.position;

        }

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

            var dir = item.transform.position - transform.position;
            dir.Normalize();

            var hit = Physics2D.Raycast(transform.position, dir, float.MaxValue, LayerMask.GetMask("Target"));

            float dist = Vector2.Distance(transform.position, hit.point);

            if (dist < minRange && hit.transform == item.transform)
            {

                trm = item.transform;
                minRange = dist;

            }

        }

        return (trm, minRange);

    }

}
