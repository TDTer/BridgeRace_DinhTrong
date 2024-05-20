using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void CloseDirectly()
    {
        Time.timeScale = 1;
        base.CloseDirectly();
    }

    public void ContinueButton()
    {
        CloseDirectly();
    }

    public void RetryButton()
    {
        LevelManager.Ins.OnRetry();
        UIManager.Ins.CloseUI<GamePlay>();
        CloseDirectly();
    }
}
