using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public struct Equipment
{
    public Sprite image;        // �� ���߿� ���������� �ٲ� ����
    public Vector3 position;    // ��ġ
    public Vector3 rotation;    // ����

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
    /// �Ŵ����� ���� �� ������Ʈ �Լ�, �ٸ� �̹����� �浹�� �Ǹ� true, �ƴϸ� false�� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns>�浹���ι�ȯ</returns>
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

    //�������� �Լ�
    private void Move()
    {
        Vector3 dir = new Vector3(0,-0.7f,0);
        if (Input.GetKey(KeyCode.LeftArrow))
            dir.x -= 1f;
        if (Input.GetKey(KeyCode.RightArrow))
            dir.x += 1f;
        _rectTransform.position = _rectTransform.position + dir;
    }

    //������ �Լ�
    private void Rotation()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rectTransform.rotation = Quaternion.Euler(0, 0, (int)(_rectTransform.eulerAngles.z - 90));
        }
    }

    //Y�� Ȯ��
    private void CheckOutLine()
    {
        if (_rectTransform.localPosition.y <= -280)
        {
            Debug.Log("�� ��");
            _isStopped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tetris"))
            _isStopped = true;
    }
}