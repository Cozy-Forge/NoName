using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public struct Equipment
{
    public Sprite image;        // 얜 나중에 아이템으로 바뀔 예정
    public Vector3 position;    // 위치
    public Vector3 rotation;    // 각도

}

public class EquipmentImg : MonoBehaviour
{
    public Equipment equipment;
    bool _isStopped = false;

    private RectTransform _rectTransform;
    private PolygonCollider2D _rect1Dimensions;
    private PolygonCollider2D _rect2Dimensions;
    private void Awake()
    {
        _rect1Dimensions = transform.GetComponent<PolygonCollider2D>();
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// 매니저가 관리 할 업데이트 함수, 다른 이미지와 충돌이 되면 true, 아니면 false를 반환한다.
    /// </summary>
    /// <returns>충돌여부반환</returns>
    public bool UpdateFunction()
    {
        if(!_isStopped)
        {
            Move();
            Rotation();
            CheckOutLine();
            return false;
        }
        else
        {
            PriortyQueueEquipment.Instance.Push(this);
            return true;
        }
    }

    //내려가는 함수
    private void Move()
    {
        Vector3 dir = new Vector3(0,-0.7f,0);
        if (Input.GetKey(KeyCode.LeftArrow))
            dir.x -= 1f;
        if (Input.GetKey(KeyCode.RightArrow))
            dir.x += 1f;
        _rectTransform.position = _rectTransform.position + dir;
    }

    //돌리는 함수
    private void Rotation()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rectTransform.rotation = Quaternion.Euler(0, 0, (int)(_rectTransform.eulerAngles.z - 90));
        }
    }

    //Y값 확인
    private void CheckOutLine()
    {
        if (_rectTransform.localPosition.y <= -280)
        {
            Debug.Log("맨 밑");
            _isStopped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tetris"))
            _isStopped = true;
    }
}