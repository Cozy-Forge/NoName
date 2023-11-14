using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Panel_Type
{
    None,
    Control,
    Sound,
    Video,
}

[System.Serializable]
public struct PanelInfo
{
    public Panel_Type Type;
    public Panel MyPanel;

    public Button PanelOpenBtn;        
}

public class EscPanel : Panel
{
    // panel_type - panel
    private Dictionary<Panel_Type, Panel> _panels = new Dictionary<Panel_Type, Panel>();

    [Header("IntroUIController")]
    [SerializeField]
    private PlayerUIReader _introUIController;

    [Header("Panel Info")]
    [SerializeField]
    private List<PanelInfo> _panelInfos = new List<PanelInfo>();
    [SerializeField]
    private RectTransform _panelContainer;
    [SerializeField]
    private RectTransform _escPanelBackground;

    private bool _showPanel             = false; // is showed other panel
    private Panel_Type _activePanel     = Panel_Type.None; // current active panel

    protected override void Awake()
    {
        base.Awake();

        //RegisterPanel(Panel_Type.None, this);

        foreach (var info in _panelInfos)
        {
            RegisterPanel(info.Type, info.MyPanel);

            //Btn에다가 연결
            info.PanelOpenBtn.onClick.AddListener(() =>
            {
                PopupPanel(info.Type);
            });
        }
    }

    private void RegisterPanel(Panel_Type type, Panel panel)
    {
        if (_panels.ContainsKey(type))
        {
            Debug.LogError("This type of panel is already contain");
            return;
        }

        _panels.Add(type, panel);
    }

    public void PopupPanel(Panel_Type type)
    {
        if (_panels.ContainsKey(type) == false)
        {
            Debug.LogError("This type of panel does not exist.");
            return;
        }

        // already showed panel off
        if (_showPanel == true)
            _panels[_activePanel].ShowOff();
        else
        {
            _panelContainer.gameObject.SetActive(true);
            _showPanel = true;
        }

        _activePanel = type;
        _panels[type].ShowOn();
    }
    
    // current active panel off
    public void ShowOffPanel()
    {
        if (_showPanel == false)
            return;

        _showPanel = false;
        _panelContainer.gameObject.SetActive(false);
        _panels[_activePanel].ShowOff();
        _activePanel = Panel_Type.None;
    }

    public override void ShowOn(bool isPopUpEffect = true)
    {
        if (_escPanelBackground.gameObject.activeSelf == true)
            return;

        _escPanelBackground.gameObject.SetActive(true);
        base.ShowOn(isPopUpEffect);
    }

    public override void ShowOff()
    {
        if (_showPanel == true)
            ShowOffPanel();

        _introUIController.SetEnable(true);
        _escPanelBackground.gameObject.SetActive(false);
        base.ShowOff();
    }
}