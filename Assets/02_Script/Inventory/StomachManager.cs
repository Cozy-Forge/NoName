using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachManager : MonoBehaviour
{
    public static StomachManager Instance;

    [SerializeField]private EquipmentImg _curEquipmentImg;

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
        if(_curEquipmentImg != null)
        {
            if(_curEquipmentImg.UpdateFunction())
                _curEquipmentImg = null;
        }
    }

    public void SetEquipmentImg(EquipmentImg equipmentImg) => _curEquipmentImg = equipmentImg;
}
