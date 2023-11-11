using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{

    [SerializeField] private int _minMapSize, _maxMapSize;
    [SerializeField] private int _roadSize;
    [SerializeField] private int _minRoadLength, _maxRoadLength;

    private List<Room> _rooms = new();

    public struct Room
    {

        public Room(Vector2Int position, List<Vector2Int> dirList, int width, int height)
        {

            _rect = new RectInt(0, 0, width, height);
            _rect.position = position;
            _dirList = dirList;

        }

        public List<Vector2Int> _dirList;
        public RectInt _rect;

    }

    private void Start()
    {
        
        CreateRooms();

    }

    private void CreateRooms()
    {

        float percent = 100;

        Queue<Room> notIncludedRoom = new();
        notIncludedRoom.Enqueue(new Room(Vector2Int.zero, new List<Vector2Int>(), 20, 20));
        
        while(percent < 0)
        {

            var dirs = GetRamdomDir(percent);

            foreach(var dir in dirs)
            {

                

            }

            percent -= 10;

        }

    }

    private List<Vector2Int> GetRamdomDir(float percent)
    {

        Vector2Int[] dirs = new Vector2Int[] { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

        List<Vector2Int> ans = new List<Vector2Int>();

        foreach(var dir in dirs)
        {

            if(Random.Range(0, 100f) < percent)
            {

                ans.Add(dir);

            }

        }

        return ans;

    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        
        var old = Gizmos.color;

        Gizmos.color = Color.red;

        foreach(var room in _rooms)
        {

            Gizmos.DrawWireCube((Vector2)room._rect.position, (Vector2)room._rect.size);

        }


        Gizmos.color = old;
    }

#endif

}
