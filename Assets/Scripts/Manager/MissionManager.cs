using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject MissionMessageUI;

    public static MissionManager instance;

    private UIManager uiManager;
    private bool hasMission = false;
    private InteractType curZone;

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

    #region 미션 UI 관련
    public void ShowMissionDesc(InteractType type)
    {
        if (hasMission) return; // 미션을 진행 중이면 X

        curZone = type;

        string msg = "";
        switch(type)
        {
            case InteractType.BoxMission:
                msg = "물건을 진열해야 합니다.\n도전하시겠습니까?";
                break;
            case InteractType.MiniGame:
                msg = "진상 손님으로부터 도망갈 수 있습니다.";
                break;
        }
        ShowGetMissionUI(msg);
    }
    public void MissionStart()
    {
        OffMissionUI();

        switch(curZone)
        {
            case InteractType.BoxMission:
                hasMission = true;
                // 상자 들고있게 변경
                // 내려둘 곳 표시
                break;
            case InteractType.MiniGame:
                // 미니게임 시작
                SceneManager.LoadScene("MiniGameScene");
                break;
            default:
                break;
        }

    }
    public void CompleteMission()
    {
        hasMission = false;
        Debug.Log("미션 완료");

        // TO DO : 보상, 켰던 UI Off 등 미션 완료 시 할 일들
    }
    private void ShowGetMissionUI(string msg)
    {
        MissionMessageUI.GetComponentInChildren<TextMeshProUGUI>().text = msg;
        MissionMessageUI.SetActive(true);
    }
    public void OffMissionUI()
    {
        MissionMessageUI.SetActive(false);
    }
    #endregion

}
