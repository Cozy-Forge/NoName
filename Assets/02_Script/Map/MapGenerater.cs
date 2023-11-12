using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerater : MonoBehaviour
{

    [SerializeField] private int _minMapSize, _maxMapSize;
    [SerializeField] private int _roadSize;
    [SerializeField] private int _minRoadLength, _maxRoadLength;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tile;

    private List<Room> _rooms = new();

    public struct Room
    {

        public Vector2 _pos, _size;
        public Vector2 MinPoint => _pos - _size / 2;
        public Vector2 MaxPoint => _pos + _size / 2;

        public List<(Room, Vector2)> _connectRoom;

        public Room(Vector2 pos, Vector2 size)
        {

            _pos = pos;
            _size = size;
            _connectRoom = new List<(Room, Vector2)>();

        }

        public bool Overlap(Room room)
        {

            return (MinPoint.x <= room.MaxPoint.x && room.MinPoint.x <= MaxPoint.x) &&
                (MinPoint.y <= room.MaxPoint.y && room.MinPoint.y <= MaxPoint.y);

        }

    }

    private void Start()
    {
        
        CreateRooms();
        DrawTile();
        DrawOutline();

    }

    private void DrawTile()
    {

        Vector3Int point = Vector3Int.zero;

        foreach(var room in _rooms)
        {

            for(int x = (int)room.MinPoint.x; x < room.MaxPoint.x; x++)
            {

                for (int y = (int)room.MinPoint.y; y < room.MaxPoint.y; y++)
                {

                    point.x = x;
                    point.y = y;
                    _tilemap.SetTile(point, _tile);

                }

            }

            foreach (var cr in room._connectRoom)
            {

                var minX = Mathf.Min(cr.Item1._pos.x, room._pos.x);
                var maxX = Mathf.Max(cr.Item1._pos.x, room._pos.x);
                var minY = Mathf.Min(cr.Item1._pos.y, room._pos.y);
                var maxY = Mathf.Max(cr.Item1._pos.y, room._pos.y);

                if (cr.Item2.x != 0)
                {

                    for (int x = (int)minX; x < maxX; x++)
                    {

                        for(int i = -1; i <= 1; i++)
                        {

                            point.x = x;
                            point.y = (int)room._pos.y + i;
                            _tilemap.SetTile(point, _tile);

                        }

                    }

                }
                else
                {

                    for (int y = (int)minY; y < maxY; y++)
                    {

                        for (int i = -1; i <= 1; i++)
                        {

                            point.x = (int)room._pos.x + i;
                            point.y = y;
                            _tilemap.SetTile(point, _tile);

                        }

                    }

                }

            }

        }


    }

    private void CreateRooms()
    {

        float percent = 100;

        Queue<Room> notIncludedRoom = new();
        notIncludedRoom.Enqueue(new Room(Vector2.zero, Vector2.one * 20));
        
        while(notIncludedRoom.Count > 0)
        {

            var dirs = GetRamdomDir(percent);
            var curRoom = notIncludedRoom.Dequeue();

            foreach (var dir in dirs)
            {

                var roadLen = Random.Range(_minRoadLength, _maxRoadLength);
                var width = Random.Range(_minMapSize, _maxMapSize);
                var height = Random.Range(_minMapSize, _maxMapSize);

                var curDir = dir * roadLen;

                if (dir.x != 0)
                {

                    curDir.x += (width / 2) * dir.x;

                }
                else
                {

                    curDir.y += (height / 2) *  dir.y;

                }

                bool isIn = false;

                var room = new Room(curDir + curRoom._pos, new Vector2(width, height));

                foreach (var item in _rooms)
                {

                    if (item.Overlap(room))
                    {

                        isIn = true;
                        break;

                    }

                }

                foreach (var item in notIncludedRoom)
                {

                    if (item.Overlap(room))
                    {

                        isIn = true;
                        break;

                    }

                }

                if (!isIn)
                {

                    notIncludedRoom.Enqueue(room);
                    curRoom._connectRoom.Add((room, dir));

                }

            }

            _rooms.Add(curRoom);

            percent -= 5;

        }

    }

    private void DrawOutline()
    {

        Vector3Int point = Vector3Int.zero;

        foreach (var room in _rooms)
        {

            for (int x = (int)room.MinPoint.x; x < room.MaxPoint.x; x++)
            {

                for (int y = (int)room.MinPoint.y; y < room.MaxPoint.y; y++)
                {

                    point.x = x;
                    point.y = y;
                    _tilemap.SetTile(point, _tile);

                }

            }

            foreach (var cr in room._connectRoom)
            {

                var minX = Mathf.Min(cr.Item1._pos.x, room._pos.x);
                var maxX = Mathf.Max(cr.Item1._pos.x, room._pos.x);
                var minY = Mathf.Min(cr.Item1._pos.y, room._pos.y);
                var maxY = Mathf.Max(cr.Item1._pos.y, room._pos.y);

                if (cr.Item2.x != 0)
                {

                    for (int x = (int)minX; x < maxX; x++)
                    {

                        for (int i = -1; i <= 1; i++)
                        {

                            point.x = x;
                            point.y = (int)room._pos.y + i;
                            _tilemap.SetTile(point, _tile);

                        }

                    }

                }
                else
                {

                    for (int y = (int)minY; y < maxY; y++)
                    {

                        for (int i = -1; i <= 1; i++)
                        {

                            point.x = (int)room._pos.x + i;
                            point.y = y;
                            _tilemap.SetTile(point, _tile);

                        }

                    }

                }

            }

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
        
        Gizmos.color = Color.yellow;

        foreach(var room in _rooms)
        {

            Gizmos.DrawWireCube(room._pos, room._size);

        }

    }

#endif

}
