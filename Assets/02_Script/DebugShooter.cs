using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShooter : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab, debugPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {

            StartCoroutine(Ins(bulletPrefab));

        }

        if(Input.GetKeyDown(KeyCode.R))
        {

            StartCoroutine(Ins(debugPrefab));

        }

    }

    private IEnumerator Ins(GameObject prefab)
    {

        for(int i = 0; i <= 10000; i++)
        {

            yield return null;
            if(i % 1000 == 0)
            {

                Debug.Log($"bulletCount {i} : fps {1/Time.deltaTime}");

            }
            Instantiate(prefab, transform.position, Quaternion.identity);

        }


    }

}
