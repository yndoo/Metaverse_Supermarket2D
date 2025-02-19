using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject MissionMessageUI;

    public static MissionManager instance;
    public bool CanGetMission { get; set; }

    private bool hasMission = false;
    private InteractType curZone;
    private float boxWeight;

    private UIManager uiManager;
    private ResourceController resourceController;
    private AnimationHandler animationHandler;
    private RandomFood randomBox;
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

        resourceController = FindObjectOfType<ResourceController>();
        animationHandler = FindObjectOfType<AnimationHandler>();
    }
    public void AnimHandlerInit(AnimationHandler ah)
    {
        animationHandler = ah;
    }
    public void ResourceInit(ResourceController rc)
    {
        resourceController = rc;
    }

    public bool DoingMission()
    {
        return hasMission;
    }

    #region 미션 UI 관련
    public void ShowMissionDesc(InteractType type)
    {
        if (hasMission) return; // 미션을 진행 중이면 X

        CanGetMission = true;
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
    /// <summary>
    /// 미션 수락 시 실행
    /// </summary>
    public void MissionStart()
    {
        OffMissionUI();

        switch(curZone)
        {
            case InteractType.BoxMission:
                BoxMissionStart();
                break;
            case InteractType.MiniGame:
                // 미니게임 시작 
                SceneManager.LoadScene("MiniGameScene");
                break;
            default:
                break;
        }

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

    /// <summary>
    /// 창고 물건 진열하는 미션 시작 행동 ('박스미션' 으로 통칭)
    /// </summary>
    private void BoxMissionStart()
    {
        hasMission = true;
        // 상자 무게에 따라 이동 속도 감소
        boxWeight = Random.Range(0f, 4f);
        resourceController.MoveSpeed -= boxWeight;
        // 상자 들고있게 변경
        animationHandler.SwitchHolding(true);
        if (randomBox == null)
        {
            randomBox = FindObjectOfType<RandomFood>(true);
            //randomBox.gameObject.SetActive(true);
        }
        randomBox.RandomOn();
        // 내려둘 곳 표시
    }
    /// <summary>
    /// 박스미션 완료 행동
    /// </summary>
    public void BoxMissionComplete()
    {
        hasMission = false;
        animationHandler.SwitchHolding(false);
        randomBox.gameObject.SetActive(false);
        // 미션 완료 보상
        resourceController.MoveSpeed += boxWeight;
        int rewardCoin = (int)(Random.Range(0, boxWeight) * 10);
        resourceController.AddCoin(rewardCoin);
    }
}
