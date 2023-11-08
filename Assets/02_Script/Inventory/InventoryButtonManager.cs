using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryButtonManager : MonoBehaviour
{
    [Header("�г�")]
    [SerializeField] private RectTransform _inventroyPanel;
    [SerializeField] private RectTransform _inventroy;
    [SerializeField] private RectTransform _item;

    [Header("���� �ӵ�")]
    [SerializeField] private float _durationSpeed;

    bool _isMoved = false;
    bool _isShow = false;

    private Vector3 originVec = new Vector3(0,0,0);
    private Vector3 hiddenVec = new Vector3(1000, 0, 0);

    private WaitForSeconds wft;

    private void Awake()
    {
        wft = new WaitForSeconds(_durationSpeed);
    }

    private void Update() //�����ִ� �ڵ� ���߿� ��ǲ�ý������� �Űܾ� ��
    {
        if(Input.GetKeyDown(KeyCode.E))
            OpenInventoryPanel();
    }

    /// <summary>
    /// �κ��丮 �г��� ����
    /// </summary>
    public void OpenInventoryPanel()
    {
        if(!_isMoved && !_isShow)
        {
            _isShow = true;
            _isMoved = true;
            _inventroyPanel.DOAnchorPos(originVec,_durationSpeed);
            StartCoroutine(DelayTime());
        }
    }

    /// <summary>
    /// �κ��丮 �г��� �ݴ´�
    /// </summary>
    public void CloseInventoryPanel()
    {
        if (!_isMoved && _isShow)
        {
            _isShow = false;
            _isMoved = true;
            _inventroyPanel.DOAnchorPos(hiddenVec, _durationSpeed);
            StartCoroutine(DelayTime());
        }
    }

    /// <summary>
    /// ������ �������� ������ <- ��Ʈ����
    /// </summary>
    public void ShowItem()
    {
        _item.gameObject.SetActive(true);
        _inventroy.gameObject.SetActive(false);
    }

    /// <summary>
    /// �κ��� ������ 
    /// </summary>
    public void ShowInven()
    {
        _item.gameObject.SetActive(false);
        _inventroy.gameObject.SetActive(true);
    }

    IEnumerator DelayTime()
    {
        yield return wft;
        _isMoved = false;
    }
}
