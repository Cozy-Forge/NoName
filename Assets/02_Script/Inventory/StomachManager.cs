using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachManager : MonoBehaviour
{
    public static StomachManager Instance;

    [SerializeField]private EquipmentImg _curEquipmentImg;
    [SerializeField] private GameObject _stomach;

    [SerializeField] private List<EquipmentImg> testList;

    private void Awake()
    {
        PriortyQueueEquipment.Instance = new PriortyQueueEquipment();

        if(Instance == null )
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : StomachManager is multiply running!");
            Destroy(transform);
        }
    }

    private void Update()
    {
        Test();
        if(_curEquipmentImg != null)
        {
            if(_curEquipmentImg.UpdateFunction())
                _curEquipmentImg = null;
        }
    }

    public void Test()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _curEquipmentImg = Instantiate(testList[Random.Range(0, testList.Count - 1)],_stomach.transform);
        }
    }

    public void SetEquipmentImg(EquipmentImg equipmentImg) => _curEquipmentImg = equipmentImg;
}
