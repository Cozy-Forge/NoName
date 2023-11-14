using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomData : MonoBehaviour
{

    [field: SerializeField] public int Width { get; private set; }
    [field: SerializeField] public int Height { get; private set; }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3Int(Width, Height));

    }

#endif

}
