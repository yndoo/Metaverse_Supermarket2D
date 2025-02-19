using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject MissionMessageUI;

    public static MissionManager instance;

    private UIManager uiManager;
    private bool hasMission = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if(uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    public bool DoingMission()
    {
        return hasMission;
    }

    #region 상자미션 UI
    public void ShowMissionDesc()
    {
        if (hasMission) return; // 이미 진행 중이면 X
        ShowGetMissionUI();
    }
    public void MissionStart()
    {
        hasMission = true;
        OffMissionUI();

        // 상자 들고있게 변경

        // 내려둘 곳 표시
    }
    public void CompleteMission()
    {
        hasMission = false;
        Debug.Log("미션 완료");
        // 보상, 켰던 UI Off 등.
    }
    private void ShowGetMissionUI()
    {
        MissionMessageUI.SetActive(true);
    }
    public void OffMissionUI()
    {
        MissionMessageUI.SetActive(false);
    }
    #endregion

}
