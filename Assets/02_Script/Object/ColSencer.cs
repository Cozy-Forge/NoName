using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColSencer : MonoBehaviour
{

    [SerializeField] private UnityEvent _enterEvent, _exitEvent;
    [SerializeField] private string _tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag))
        {

            _enterEvent?.Invoke();

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag(_tag))
        {

            _exitEvent?.Invoke();

        }

    }

}
