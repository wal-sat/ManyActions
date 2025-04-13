using UnityEngine;

public enum StageSelectSceneStatus { menu, menuConfirm, option, optionConfirm, setting }

public class StageSelectInputManager : MonoBehaviour
{
    [SerializeField] StageSelectMenu stageSelectMenu;
    [SerializeField] StageSelectMenuConfirm stageSelectMenuConfirm;
    [SerializeField] StageSelectOption stageSelectOption;
    [SerializeField] StageSelectOptionConfirm stageSelectOptionConfirm;
    [SerializeField] StageSelectSetting stageSelectSetting;

    private StageSelectSceneStatus _stageSelectSceneStatus;

    private bool _southPast;
    private bool _eastPast;
    private bool _westPast;
    private bool _northPast;
    private bool _upPast;
    private bool _downPast;
    private bool _leftPast;
    private bool _rightPast;
    private bool _LPast;
    private bool _RPast;
    private bool _optionPast;

    private void Awake()
    {
        stageSelectMenu.ChangeStatus = ChangeStatus;
        stageSelectMenuConfirm.ChangeStatus = ChangeStatus;
        stageSelectOption.ChangeStatus = ChangeStatus;
        stageSelectOptionConfirm.ChangeStatus = ChangeStatus;
        stageSelectSetting.ChangeStatus = ChangeStatus;
    }

    private void Start()
    {
        #if UNITY_EDITOR
            S_InputSystem._instance.canInput = true;
        #endif

        S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);
        
        ChangeStatus(StageSelectSceneStatus.menu);
    }

    private void Update()
    {
        if (S_InputSystem._instance.isPushingSouth && !_southPast) South();
        else if (!S_InputSystem._instance.isPushingSouth && _southPast) _southPast = false;

        if (S_InputSystem._instance.isPushingEast && !_eastPast) East();
        else if (!S_InputSystem._instance.isPushingEast && _eastPast) _eastPast = false;

        if (S_InputSystem._instance.isPushingWest && !_westPast) West();
        else if (!S_InputSystem._instance.isPushingWest && _westPast) _westPast = false;

        if (S_InputSystem._instance.isPushingNorth && !_northPast) North();
        else if (!S_InputSystem._instance.isPushingNorth && _northPast) _northPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.up && !_upPast) Up();
        else if (S_InputSystem._instance.leftDirection != Vector2.up && _upPast) _upPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.down && !_downPast) Down();
        else if (S_InputSystem._instance.leftDirection != Vector2.down && _downPast) _downPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.left && !_leftPast) Left();
        else if (S_InputSystem._instance.leftDirection != Vector2.left && _leftPast) _leftPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.right && !_rightPast) Right();
        else if (S_InputSystem._instance.leftDirection != Vector2.right && _rightPast) _rightPast = false;

        if (S_InputSystem._instance.isPushingL1 && !_LPast) L();
        else if (!S_InputSystem._instance.isPushingL1 && _LPast) _LPast = false;

        if (S_InputSystem._instance.isPushingR1 && !_RPast) R();
        else if (!S_InputSystem._instance.isPushingR1 && _RPast) _RPast = false;

        if (S_InputSystem._instance.isPushingOption && !_optionPast) Option();
        else if (!S_InputSystem._instance.isPushingOption && _optionPast) _optionPast = false;
    }

    private void ChangeStatus(StageSelectSceneStatus status)
    {
        stageSelectMenu.stageSelectMenuUIToolkit.PanelVisibility(false);
        stageSelectMenuConfirm.stageSelectMenuConfirmUIToolkit.PanelOpen(false);
        stageSelectOption.stageSelectOptionUIToolkit.PanelOpen(false);
        stageSelectOptionConfirm.stageSelectOptionConfirmUIToolkit.PanelOpen(false);
        S_SettingInfo._instance.OpenOrCloseSettingPanel(false);
        
        _stageSelectSceneStatus = status;
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.stageSelectMenuUIToolkit.PanelVisibility(true);
                break;
            case StageSelectSceneStatus.menuConfirm:
                stageSelectMenu.stageSelectMenuUIToolkit.PanelVisibility(true);
                stageSelectMenuConfirm.stageSelectMenuConfirmUIToolkit.PanelOpen(true);
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.stageSelectOptionUIToolkit.PanelOpen(true);
                break;
            case StageSelectSceneStatus.optionConfirm:
                stageSelectOption.stageSelectOptionUIToolkit.PanelOpen(true);
                stageSelectOptionConfirm.stageSelectOptionConfirmUIToolkit.PanelOpen(true);
                break;
            case StageSelectSceneStatus.setting:
                stageSelectOption.stageSelectOptionUIToolkit.PanelOpen(true);
                S_SettingInfo._instance.OpenOrCloseSettingPanel(true);
                break;
        }
    }

    private void South()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorSelect();
                break;
            case StageSelectSceneStatus.menuConfirm:
                stageSelectMenuConfirm.CursorSelect();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorSelect();
                break;
            case StageSelectSceneStatus.optionConfirm:
                stageSelectOptionConfirm.CursorSelect();
                break;
            case StageSelectSceneStatus.setting:
                stageSelectSetting.CursorSelect();
                break;
        }
        _southPast = true;
    }
    private void East()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorCancel();
                break;
            case StageSelectSceneStatus.menuConfirm:
                stageSelectMenuConfirm.CursorCancel();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorCancel();
                break;
            case StageSelectSceneStatus.optionConfirm:
                stageSelectOptionConfirm.CursorCancel();
                break;
            case StageSelectSceneStatus.setting:
                stageSelectSetting.CursorCancel();
                break;
        }
        _eastPast = true;
    }
    private void West()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.West();
                break;
        }
        _westPast = true;
    }
    private void North()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.North();
                break;
        }
        _northPast = true;
    }
    private void Up()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorUp();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorUp();
                break;
            case StageSelectSceneStatus.setting:
                stageSelectSetting.CursorUp();
                break;
        }
        _upPast = true;
    }
    private void Down()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorDown();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorDown();
                break;
            case StageSelectSceneStatus.setting:
                stageSelectSetting.CursorDown();
                break;
        }
        _downPast = true;
    }
    private void Left()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menuConfirm:
                stageSelectMenuConfirm.CursorLeft();
                break;
            case StageSelectSceneStatus.optionConfirm:
                stageSelectOptionConfirm.CursorLeft();
                break;
            case StageSelectSceneStatus.setting:
                stageSelectSetting.CursorLeft();
                break;
        }
        _leftPast = true;
    }
    private void Right()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menuConfirm:
                stageSelectMenuConfirm.CursorRight();
                break;
            case StageSelectSceneStatus.optionConfirm:
                stageSelectOptionConfirm.CursorRight();
                break;
            case StageSelectSceneStatus.setting:
                stageSelectSetting.CursorRight();
                break;
        }
        _rightPast = true;
    }
    private void L()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.L();
                break;
        }
        _LPast = true;
    }
    private void R()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.R();
                break;
        }
        _RPast = true;
    }
    private void Option()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.Option();
                break;
            case StageSelectSceneStatus.menuConfirm:
                stageSelectMenuConfirm.Option();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.Option();
                break;
            case StageSelectSceneStatus.optionConfirm:
                stageSelectOptionConfirm.Option();
                break;
            case StageSelectSceneStatus.setting:
                stageSelectSetting.Option();
                break;
        }
        _optionPast = true;
    }
}
