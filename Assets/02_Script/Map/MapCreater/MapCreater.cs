using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCreater : MonoBehaviour
{

    [SerializeField] private int _minRoad, _maxRoad;
    [SerializeField] private Tilemap _roadTilemap, _wallTilemap;
    [SerializeField] private TileBase _baseTile, _wallTile, _wallSideXTile, _wallSideYTile_T1, _wallSideYTile_T2;
    private List<RoomData> _rooms = new List<RoomData>();
    private List<RoomBindData> _roomBind = new();

    public struct RoomBindData
    {

        public RoomData room;
        public List<(Vector2Int point, Vector2Int dir)> points;
        public Vector2Int oldDir;

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

        public void CloseUnUsedRoad(params TileBase[] tile)
        {

            Vector2Int[] dirs = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right };

            foreach(var dir in dirs)
            {

                if(points.Find(x => x.dir == dir).dir != default || oldDir == -dir) continue;

                var wallTileMap = room.transform.Find("Wall").GetComponent<Tilemap>();
                var groundTileMap = room.transform.Find("Ground").GetComponent<Tilemap>();

                if (dir.x != 0)
                {

                    var point = dir.x == 1 ? (int)wallTileMap.transform.localPosition.x + room.Width / 2 : 
                        ((int)wallTileMap.transform.localPosition.x - room.Width / 2) - 1;
                    var curtile = dir.x == 1 ? tile[4] : tile[3];//

                    for (int i = -2; i < 4; i++)
                    {

                        wallTileMap.SetTile(new Vector3Int(point, (int)wallTileMap.transform.localPosition.y - i), curtile);
                        groundTileMap.SetTile(new Vector3Int(point, (int)wallTileMap.transform.localPosition.y - i), null);

                    }

                }

            }

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
        CloseMap();

    }

    private void CloseMap()
    {

        foreach(var item in _roomBind)
        {

            item.CloseUnUsedRoad(_baseTile, _wallTile, _wallSideXTile, _wallSideYTile_T1, _wallSideYTile_T2);

        }

    }

    private void DrawRoad()
    {

        foreach(var room in _roomBind)
        {

            for (int iters = 0; iters < room.points.Count; iters++)
            {
                var point = room.points[iters];
                if (point.dir.x != 0)
                {

                    var val1 = point.dir.x == 1 ? (int)(room.room.transform.position.x + room.room.Width / 2) : (int)(room.room.transform.position.x - room.room.Width / 2);
                    var val2 = point.point.x + (int)room.room.transform.position.x;

                    var min = Mathf.Min(val1, val2);
                    var max = Mathf.Max(val1, val2);
                    var y = (int)room.room.transform.position.y;

                    for (
                        int x = min + 1;
                        x < max - 1; x++)
                    {

                        for(int i = 0; i < 3; i++)
                        {

                            _roadTilemap.SetTile(new Vector3Int(x, y - i), _baseTile);

                        }

                        _wallTilemap.SetTile(new Vector3Int(x, y - 3), _wallTile);
                        _wallTilemap.SetTile(new Vector3Int(x, y + 1), _wallTile);
                        _wallTilemap.SetTile(new Vector3Int(x, y - 2), _wallSideXTile);
                        _wallTilemap.SetTile(new Vector3Int(x, y + 2), _wallSideXTile);

                    }

                }
                else
                {

                    var val1 = point.dir.y == 1 ? (int)(room.room.transform.position.y + room.room.Height / 2) : (int)(room.room.transform.position.y - room.room.Height / 2);
                    var val2 = point.point.y + (int)room.room.transform.position.y;

                    var min = Mathf.Min(val1, val2);
                    var max = Mathf.Max(val1, val2);
                    var x = (int)room.room.transform.position.x;

                    for (
                        int y = min + 1;
                        y < max; y++)
                    {

                        for (int i = -2; i < 2; i++)
                        {

                            if(y + i == max) continue;

                            _roadTilemap.SetTile(new Vector3Int(x + i, y), _baseTile);

                        }

                        _wallTilemap.SetTile(new Vector3Int(x - 3, y), _wallSideYTile_T1);
                        _wallTilemap.SetTile(new Vector3Int(x + 2, y), _wallSideYTile_T2);

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
            var points = new List<(Vector2Int point, Vector2Int dir)>();
            var roadLens = new List<int>();

            foreach(var dir in dirs)
            {

                if (dir == room.oldDir) continue;

                int roadLen = Random.Range(_minRoad, _maxRoad);
                roadLens.Add(roadLen);

                var idx = Random.Range(0, _rooms.Count);

                var obj = Instantiate(_rooms[idx]);

                var point =  dir * (roadLen + (room.room.Width / 2 + obj.Width / 2));
                var pointEv = dir * (roadLen + (room.room.Width / 2));
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

                    points.Add((pointEv, dir));//
                    notVistidData.Enqueue((obj, dir));

                }

            }

            _roomBind.Add(new RoomBindData
            {
                
                room = room.room,
                points = points,
                oldDir = room.oldDir

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
