using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Equipment
{
    public Sprite image;        // �� ���߿� ���������� �ٲ� ����
    public Vector3 position;    // ��ġ
    public Vector3 rotation;    // ����
}

public class EquipmentImg : MonoBehaviour
{
    private RectTransform _rectTransform;
    public Equipment equipment;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// �Ŵ����� ���� �� ������Ʈ �Լ�, �ٸ� �̹����� �浹�� �Ǹ� true, �ƴϸ� false�� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns>�浹���ι�ȯ</returns>
    public bool UpdateFunction()
    {
        if(!CheckCollision())
        {
            MoveDown();
            Rotation();
            return false;
        }
        else
        {
            PriortyQueueEquipment.Instance.Push(this);
            return true;
        }
    }

    //�������� �Լ�
    private void MoveDown()
    {
        _rectTransform.position = new Vector3(_rectTransform.position.x,
            _rectTransform.position.y - 1, _rectTransform.position.z);
    }

    //������ �Լ�
    private void Rotation()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rectTransform.rotation = Quaternion.Euler(0, 0, (int)(_rectTransform.eulerAngles.z - 90));
        }
    }

    //�浹 Ȯ��
    private bool CheckCollision()
    {
        if (_rectTransform.position.y < -288)
            return true;
        RectTransform tempRect;
        List<EquipmentImg> tempList = PriortyQueueEquipment.Instance.GetEquipmentList();
        Rect rect1Dimensions = new Rect(_rectTransform.localPosition,_rectTransform.rect.size);
        Rect rect2Dimensions;

        for (int i = 0; i < tempList.Count; ++i)
        {
            tempRect = tempList[i]._rectTransform;
            rect2Dimensions = new Rect(tempRect.localPosition, tempRect.rect.size);

            if (rect1Dimensions.Overlaps(rect2Dimensions))
                return true;
        }
        return false;
    }
}