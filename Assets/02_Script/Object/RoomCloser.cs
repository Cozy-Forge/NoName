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

    }

    public void Open()
    {

        _closeRoot.SetActive(false);

    }

}
