using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI PopularText;

    protected override UIState GetUIState()
    {
        return UIState.GameUI;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
    public void UpdatePolular(int popular)
    {
        PopularText.text = popular.ToString();
    }
    public void UpdateCoin(int coin)
    {
        CoinText.text = coin.ToString();
    }
    public void UpdateAllStat(int popular, int coin)
    {
        PopularText.text = popular.ToString();
        CoinText.text = coin.ToString();
    }
}
