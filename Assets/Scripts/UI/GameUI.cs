using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{

    private void Start()
    {
    }
    protected override UIState GetUIState()
    {
        return UIState.GameUI;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

}
