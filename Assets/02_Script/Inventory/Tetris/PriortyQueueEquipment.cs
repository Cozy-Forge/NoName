using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PriortyQueueEquipment 
{
    public static PriortyQueueEquipment Instance = null;

    //List<EquipmentImg> _equipmentList = new List<EquipmentImg>(); // 우선순위 리스트
    //int size => _equipmentList.Count; // 리스트 사이즈

    ///// <summary>
    ///// 생성자 인스턴스 예외처리
    ///// </summary>
    //public PriortyQueueEquipment() 
    //{
    //    if (Instance != null)
    //        Debug.LogError($"PriortyQueueEquipment is multiply running!");
    //}

    ///// <summary>
    ///// YPosition을 기준으로 아이템을 리스트에 넣는다.
    ///// </summary>
    ///// <param name="equipment">넣을 아이템</param>
    //public void Push(EquipmentImg equipmentImg)
    //{
    //    for (int i = 0; i < size; i++)
    //    {
    //        if(equipmentImg.equipment.position.y < _equipmentList[i].equipment.position.y)
    //        {
    //            _equipmentList.Insert(i, equipmentImg);
    //            return;
    //        }
    //    }
    //    _equipmentList.Add(equipmentImg);
    //}

    ///// <summary>
    ///// 매개변수 cnt만큼 리스트에서 값을 뺀다.
    ///// </summary>
    ///// <param name="cnt"></param>
    //public void RandomPop(int cnt)
    //{
    //    int tempIdx;
    //    for(int i = 0; i < cnt; i++)
    //    {
    //        tempIdx = Random.Range(0,size - 1);
    //        Pop(tempIdx);
    //    }
    //}

    ///// <summary>
    ///// 매개변수 idx값이 빠짐
    ///// </summary>
    ///// <param name="idx"></param>
    //public void Pop(int idx)
    //{
    //    Object.Destroy(_equipmentList[idx]);
    //    _equipmentList.RemoveAt(idx);
    //}

    ///// <summary>
    ///// 리스트 반환, 반환된 애 건들면 안됨 진짜 안됨 진짜진짜 안됨
    ///// </summary>
    ///// <returns>리스트 리턴</returns>
    //public List<EquipmentImg> GetEquipmentList()
    //{
    //    return _equipmentList;
    //}
}