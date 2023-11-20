using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DropTable")]
public class DropTableSO : ScriptableObject
{

    [SerializeField] private List<GameObject> _dropTable = new();

    public GameObject GetRandomDropObjectInstance()
    {

        int idx = Random.Range(0, _dropTable.Count);

        return _dropTable[idx];

    }

}
