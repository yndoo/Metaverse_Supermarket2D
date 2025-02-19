using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject GetMissionUI;

    public static MissionManager instance;

    private UIManager uiManager;
    private bool hasMission = false;

    private void Awake()
    {
        instance = this;

        uiManager = FindObjectOfType<UIManager>();
    }

    // 미션 보여주기
    public void ShowMissionDesc()
    {
        if (hasMission) return; // 이미 진행 중이면 X
        ShowGetMissionUI();
    }

    // 미션 주기
    public void MissionStart()
    {
        hasMission = true;
        OffMissionUI();

        // 상자 들고있게 변경

        // 내려둘 곳 표시
    }

    // 미션 완료
    public void CompleteMission()
    {
        hasMission = false;

        // 보상, 켰던 UI Off 등.
    }

    private void ShowGetMissionUI()
    {
        GetMissionUI.SetActive(true);
    }

    public void OffMissionUI()
    {
        GetMissionUI.SetActive(false);
    }
}
