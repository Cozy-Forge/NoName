using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryButtonManager : MonoBehaviour
{
    public static InventoryButtonManager Instance;

    [Header("�г�")]
    [SerializeField] private RectTransform _inventroyPanel;
    [SerializeField] private RectTransform _inventroy;
    [SerializeField] private RectTransform _item;
    [SerializeField] private RectTransform _craftingPanel;


    [Header("���� �ӵ�")]
    [SerializeField] private float _durationSpeed;

    [SerializeField] private PlayerInputReader _inputReader;

    bool _isMoved = false;
    bool _isShow = false;

    private AudioSource _invenAudio;

    private Vector3 originVec = new Vector3(0, 0, 0);
    private Vector3 hiddenVec = new Vector3(1000, 0, 0);

    private WaitForSeconds _wft;

    private void Awake()
    {
        #region �̱���
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"{transform} : InventoryButtonManager is Multiple running!");
            Destroy(gameObject);
        }
        #endregion
        _invenAudio = GetComponent<AudioSource>();
        _wft = new WaitForSeconds(_durationSpeed);
    }

    //�����ִ� �ڵ� ���߿� ��ǲ�ý������� �Űܾ� ��
    private void Update() 
    {
        InputKey();
    }

    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isShow)
            {
                CloseInventoryPanel();
            }
            else
            {
                OpenInventoryPanel();
            }
        }
    }

    /// <summary>
    /// �κ��丮 �г��� ����
    /// </summary>
    public void OpenInventoryPanel()
    {
        if (!_isMoved && !_isShow)
        {
            _isShow = true;
            _isMoved = true;
            _inventroyPanel.DOAnchorPos(originVec, _durationSpeed);
            _invenAudio.Play();
            StartCoroutine(DelayTime());
            _inputReader.SetEnable(false);
        }
    }

    //������ �κ� ����
    public void CompulsionOpenTetris()
    {
        transform.DOKill();
        _isShow = true;
        _isMoved = true;
        _inventroyPanel.DOAnchorPos(originVec, _durationSpeed);
        ShowItem();
        StartCoroutine(DelayTime());
    }

    /// <summary>
    /// �κ��丮 �г��� �ݴ´�
    /// </summary>
    public void CloseInventoryPanel()
    {
        if (BlockManager.Instance != null && BlockManager.Instance.selectBlock != null)
            return;
        
        if (PriortyQueueBlock.Instance != null && PriortyQueueBlock.Instance.isImgDestroy)
            return;

        if (!_isMoved && _isShow)
        {
            _isShow = false;
            _isMoved = true;
            _craftingPanel.gameObject.SetActive(false);
            _inventroyPanel.DOAnchorPos(hiddenVec, _durationSpeed);
            _invenAudio.Play();
            StartCoroutine(DelayTime());
            _inputReader.SetEnable(true);
        }
    }

    /// <summary>
    /// ������ �������� ������ <- ��Ʈ����
    /// </summary>
    public void ShowItem()
    {
        if(!_item.gameObject.activeSelf)
        {
            _item.gameObject.SetActive(true);
            _inventroy.gameObject.SetActive(false);
            _invenAudio.Play();
        }
    }

    /// <summary>
    /// �κ��� ������ 
    /// </summary>
    public void ShowInven()
    {
        if (BlockManager.Instance != null && BlockManager.Instance.selectBlock != null)
            return;

        if (PriortyQueueBlock.Instance != null && PriortyQueueBlock.Instance.isImgDestroy)
            return;

        _item.gameObject.SetActive(false);
        _inventroy.gameObject.SetActive(true);
        _invenAudio.Play();
    }

    public void CraftingBtn()
    {
        _craftingPanel.gameObject.SetActive(!_craftingPanel.gameObject.activeSelf);
        _invenAudio.Play();
    }

    IEnumerator DelayTime()
    {
        yield return _wft;
        _isMoved = false;
    }
}