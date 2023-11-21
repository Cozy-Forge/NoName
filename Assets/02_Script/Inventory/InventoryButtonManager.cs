using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryButtonManager : MonoBehaviour
{
    public static InventoryButtonManager Instance;

    [Header("패널")]
    [SerializeField] private RectTransform _inventroyPanel;
    [SerializeField] private RectTransform _inventroy;
    [SerializeField] private RectTransform _item;
    [SerializeField] private RectTransform _craftingPanel;


    [Header("변경 속도")]
    [SerializeField] private float _durationSpeed;

    [SerializeField] private PlayerInputReader _inputReader;

    bool _isMoved = false;
    bool _isShow = false;

    private Vector3 originVec = new Vector3(0, 0, 0);
    private Vector3 hiddenVec = new Vector3(1000, 0, 0);

    private WaitForSeconds wft;

    private void Awake()
    {
        #region 싱글톤
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
        wft = new WaitForSeconds(_durationSpeed);
    }

    //여기있는 코드 나중에 인풋시스템으로 옮겨야 댐
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
    /// 인벤토리 패널을 연다
    /// </summary>
    public void OpenInventoryPanel()
    {
        if (!_isMoved && !_isShow)
        {
            _isShow = true;
            _isMoved = true;
            _inventroyPanel.DOAnchorPos(originVec, _durationSpeed);
            StartCoroutine(DelayTime());
            _inputReader.SetEnable(false);
        }
    }

    //강제로 인벤 열기
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
    /// 인벤토리 패널을 닫는다
    /// </summary>
    public void CloseInventoryPanel()
    {
        if (BlockManager.Instance != null && BlockManager.Instance.selectBlock != null)
        {
            return;
        }
        if (!_isMoved && _isShow)
        {
            _isShow = false;
            _isMoved = true;
            _craftingPanel.gameObject.SetActive(false);
            _inventroyPanel.DOAnchorPos(hiddenVec, _durationSpeed);
            StartCoroutine(DelayTime());
            _inputReader.SetEnable(true);
        }
    }

    /// <summary>
    /// 장착된 아이템을 보여줌 <- 테트리스
    /// </summary>
    public void ShowItem()
    {
        _item.gameObject.SetActive(true);
        _inventroy.gameObject.SetActive(false);
    }

    /// <summary>
    /// 인벤을 보여줌 
    /// </summary>
    public void ShowInven()
    {
        if (BlockManager.Instance != null && BlockManager.Instance.selectBlock != null)
        {
            return;
        }
        _item.gameObject.SetActive(false);
        _inventroy.gameObject.SetActive(true);
    }

    public void CraftingBtn()
    {
        _craftingPanel.gameObject.SetActive(!_craftingPanel.gameObject.activeSelf);
    }

    IEnumerator DelayTime()
    {
        yield return wft;
        _isMoved = false;
    }
}