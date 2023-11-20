using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomCloser : MonoBehaviour
{

    [SerializeField] private GameObject _closeRoot;

    private List<HPObject> _objs;
    private bool _isOpen;

    private void Update()
    {

        if (_isOpen)
        {

            if(_objs.Count <= 0)
            {
                
                _isOpen = false;
                Open();

            }    

        }

    }

    public void Close()
    {

        _isOpen=true;
        _closeRoot.SetActive(true);

        var arr = GetComponentsInChildren<HPObject>();
        _objs = arr.ToList();
        
        foreach(HPObject obj in arr)
        {

            obj.OnDieEvent += () =>
            {

                _objs.Remove(obj);

            };

        }

        var anime = _closeRoot.GetComponentsInChildren<Animator>();

        foreach(var ani in anime)
        {

            ani.SetTrigger("Close");

        }

    }

    public void Open()
    {


        var anime = _closeRoot.GetComponentsInChildren<Animator>();

        foreach (var ani in anime)
        {

            ani.SetTrigger("Open");

        }

        StartCoroutine(CO());

    }

    private IEnumerator CO()
    {

        yield return new WaitForSeconds(0.5f);

        _closeRoot.SetActive(false);
    }

}
