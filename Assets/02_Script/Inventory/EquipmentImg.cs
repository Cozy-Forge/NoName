using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Equipment
{
    public Sprite image;        // 얜 나중에 아이템으로 바뀔 예정
    public Vector3 position;    // 위치
    public Vector3 rotation;    // 각도
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
    /// 매니저가 관리 할 업데이트 함수, 다른 이미지와 충돌이 되면 true, 아니면 false를 반환한다.
    /// </summary>
    /// <returns>충돌여부반환</returns>
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

    //내려가는 함수
    private void MoveDown()
    {
        _rectTransform.position = new Vector3(_rectTransform.position.x,
            _rectTransform.position.y - 1, _rectTransform.position.z);
    }

    //돌리는 함수
    private void Rotation()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rectTransform.rotation = Quaternion.Euler(0, 0, (int)(_rectTransform.eulerAngles.z - 90));
        }
    }

    //충돌 확인
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