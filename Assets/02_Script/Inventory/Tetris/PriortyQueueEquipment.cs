using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PriortyQueueEquipment 
{
    public static PriortyQueueEquipment Instance = null;

    //List<EquipmentImg> _equipmentList = new List<EquipmentImg>(); // �켱���� ����Ʈ
    //int size => _equipmentList.Count; // ����Ʈ ������

    ///// <summary>
    ///// ������ �ν��Ͻ� ����ó��
    ///// </summary>
    //public PriortyQueueEquipment() 
    //{
    //    if (Instance != null)
    //        Debug.LogError($"PriortyQueueEquipment is multiply running!");
    //}

    ///// <summary>
    ///// YPosition�� �������� �������� ����Ʈ�� �ִ´�.
    ///// </summary>
    ///// <param name="equipment">���� ������</param>
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
    ///// �Ű����� cnt��ŭ ����Ʈ���� ���� ����.
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
    ///// �Ű����� idx���� ����
    ///// </summary>
    ///// <param name="idx"></param>
    //public void Pop(int idx)
    //{
    //    Object.Destroy(_equipmentList[idx]);
    //    _equipmentList.RemoveAt(idx);
    //}

    ///// <summary>
    ///// ����Ʈ ��ȯ, ��ȯ�� �� �ǵ�� �ȵ� ��¥ �ȵ� ��¥��¥ �ȵ�
    ///// </summary>
    ///// <returns>����Ʈ ����</returns>
    //public List<EquipmentImg> GetEquipmentList()
    //{
    //    return _equipmentList;
    //}
}