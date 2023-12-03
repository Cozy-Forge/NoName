using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public int width { get; private set; }
    [field: SerializeField] public int height { get; private set; }

    public List<(Item? item, Vector2Int point, List<Vector2Int> size)> items = new();

    private List<List<Item?>> container = new();

    private void Awake()
    {

        for (int y = 0; y < height; y++)
        {

            List<Item?> ls = new();

            for (int x = 0; x < width; x++)
            {

                ls.Add(null);

            }

            container.Add(ls);

        }

    }

    public bool AddItem(Item item, Vector2Int point, List<Vector2Int> size)
    {

        Debug.Log(size.Count);

        foreach (var i in size)
        {

            Vector2Int curPoint = point + i;

            if (container.Count > curPoint.y && curPoint.y >= 0)
            {

                if (container[curPoint.y].Count > curPoint.x && curPoint.x >= 0)
                {

                    if (container[curPoint.y][curPoint.x] != null)
                    {

                        return false;

                    }

                }
                else
                {

                    return false;

                }
            }
            else
            {

                return false;

            }

        }

        foreach (var i in size)
        {

            Vector2Int curPoint = point + i;
            container[curPoint.y][curPoint.x] = item;

        }

        items.Add((item, point, size));

        return true;

    }

    public void RemoveItem(Item item)
    {

        var removeItem = items.Find(x => x.item.Value.guid == item.guid);

        if (removeItem.item != null)
        {

            items.Remove(removeItem);

            foreach (var i in container)
            {

                for (int j = 0; j < i.Count; j++)
                {

                    if (i[j]?.guid == item.guid)
                    {

                        i[j] = null;

                    }

                }

            }

        }

    }

    private void Update()
    {

        Chack();

    }

    private void Chack()
    {

        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (var item in items)
        {

            HashSet<ItemType> tp = new();

            foreach (var pt in item.size)
            {

                var curPt = pt + item.point;

                foreach (var dir in dirs)
                {

                    Vector2Int dirPt = curPt + dir;

                    if (dirPt.y < container.Count && dirPt.y >= 0)
                    {

                        if (dirPt.x < container[dirPt.y].Count && dirPt.x >= 0)
                        {

                            if (container[dirPt.y][dirPt.x] != null && container[dirPt.y][dirPt.x]?.guid != item.item.Value.guid)
                            {

                                tp.Add(container[dirPt.y][dirPt.x].Value.type);

                            }

                        }

                    }

                }

            }

            foreach (var t in tp)
            {

                item.item.Value.so.Effect(t);

            }

        }

    }

}
