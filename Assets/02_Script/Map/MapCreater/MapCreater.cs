using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCreater : MonoBehaviour
{

    [SerializeField] private int _minRoad, _maxRoad;
    [SerializeField] private Tilemap _roadTilemap;
    [SerializeField] private TileBase _baseTile;
    private List<RoomData> _rooms = new List<RoomData>();
    private List<RoomBindData> _roomBind = new();

    public struct RoomBindData
    {

        public RoomData room;
        public List<Vector2Int> dirs;
        public List<float> roadLens;

        public bool Overlap(RoomData nRoom)
        {

            Rect rt = new Rect(
                room.transform.position.x - (room.Width / 2),
                room.transform.position.y - (room.Height / 2),
                room.Width,
                room.Height);
            Rect nr = new Rect(
                nRoom.transform.position.x - (nRoom.Width / 2),
                nRoom.transform.position.y - (nRoom.Height / 2),
                nRoom.Width,
                nRoom.Height);

            return rt.Overlaps(nr) || nr.Overlaps(rt);

        }

    }


    private void Awake()
    {

        _rooms = Resources.LoadAll<RoomData>("Map").ToList();

    }

    private void Start()
    {

        CreateRoom();
        DrawRoad();

    }

    private void DrawRoad()
    {

        foreach(var room in _roomBind)
        {

            foreach(var dir in room.dirs)
            {

                if(dir.x != 0)
                {

                    for(
                        int x = ((int)room.room.transform.position.x - (room.room.Width / 2)) + 1;
                        x < room.room.Width; x++)
                    {

                        for(int i = -1; i < 2; i++)
                        {

                            _roadTilemap.SetTile(new Vector3Int(x, (int)room.room.transform.position.y + i), _baseTile);

                        }

                    }

                }

            }

        }

    }

    private void CreateRoom()
    {

        Queue<(RoomData room, Vector2Int oldDir)> notVistidData = new();
        notVistidData.Enqueue((Instantiate(_rooms[0]), Vector2Int.zero));

        float percent = 100;

        while(notVistidData.Count > 0)
        {

            var room = notVistidData.Dequeue();
            var dirs = GetRamdomDir(percent);
            var dataDir = new List<Vector2Int>();
            var roadLens = new List<float>();

            foreach(var dir in dirs)
            {

                if (dir == room.oldDir) continue;

                int roadLen = Random.Range(_minRoad, _maxRoad);
                roadLens.Add(roadLen);

                var idx = Random.Range(0, _rooms.Count);

                var obj = Instantiate(_rooms[idx]);

                var point =  dir * (roadLen + (room.room.Width / 2 + obj.Width / 2));
                obj.transform.position = (Vector2)point + (Vector2)room.room.transform.position;

                bool isOverlap = false;

                foreach(var item in _roomBind)
                {

                    if (item.Overlap(obj))
                    {

                        isOverlap = true;
                        break;

                    }

                }

                foreach(var item in notVistidData)
                {

                    if (Overlap(obj, item.room))
                    {

                        isOverlap = true;
                        break;

                    }

                }

                if (isOverlap)
                {

                    Destroy(obj.gameObject);

                }
                else
                {

                    dataDir.Add(dir);
                    notVistidData.Enqueue((obj, dir));

                }

            }

            _roomBind.Add(new RoomBindData
            {
                
                room = room.room,
                dirs = dataDir,
                roadLens = roadLens,

            });
            percent--;

        }

    }

    private bool Overlap(RoomData room, RoomData nRoom)
    {
        Rect rt = new Rect(
        room.transform.position.x - (room.Width / 2),
        room.transform.position.y - (room.Height / 2),
        room.Width,
            room.Height);
        Rect nr = new Rect(
            nRoom.transform.position.x - (nRoom.Width / 2),
            nRoom.transform.position.y - (nRoom.Height / 2),
            nRoom.Width,
            nRoom.Height);

        return rt.Overlaps(nr) || nr.Overlaps(rt);

    }

    private List<Vector2Int> GetRamdomDir(float percent)
    {

        Vector2Int[] dirs = new Vector2Int[] { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

        List<Vector2Int> ans = new List<Vector2Int>();

        foreach (var dir in dirs)
        {

            if (Random.Range(0, 100f) < percent)
            {

                ans.Add(dir);

            }

        }

        return ans;

    }

}
