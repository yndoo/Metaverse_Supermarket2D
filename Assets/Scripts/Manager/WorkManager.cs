using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WorkManager : MonoBehaviour
{
    [SerializeField] private GameObject GameMessageUI;

    public static WorkManager Instance { get; private set; }

    public bool CanGetBoxMission { get; set; } // 박스 미션 받을 수 있는 상태 확인 (박스 미션 미수행 && 상호작용 상태여야 함.)
    public bool IsWorking { get; set; } // 어떤 종류든 일하는 중.
    public bool NPCWorking { get; set; } // NPC 퀘스트 운반 중(다른 일 못하는 상태)
    public InteractType CurZone { get; set; }

    public InteractController BoxMissionZone;
    public InteractController CompleteZone;

    private UIManager uiManager;
    private ResourceController resourceController;
    private AnimationHandler animationHandler;
    private RandomFood randomBox;

    private float boxWeight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

    #region 미션 UI 관련
    public void ShowMissionDesc()
    {
        if (IsWorking) return; // 미션을 진행 중이면 X

        string msg = "";
        switch(CurZone)
        {
            case InteractType.BoxMission:
                CanGetBoxMission = true;
                msg = "물건을 진열해야 합니다.\n도전하시겠습니까?";
                break;
            case InteractType.MiniGame:
                msg = "진상 손님으로부터 도망갈 수 있습니다.";
                break;
        }
        OnMessageUI(msg);
    }
    /// <summary>
    /// 미션 수락 시 실행
    /// </summary>
    public void MissionStart()
    {
        if (IsWorking || NPCWorking) return; // 미션을 진행 중이면 X

        switch (CurZone)
        {
            case InteractType.BoxMission:
                if (CanGetBoxMission)
                {
                    BoxMissionStart();
                    CanGetBoxMission = false;
                    IsWorking = true;
                }
                break;
            case InteractType.MiniGame:
                // 미니게임 시작 
                SceneManager.LoadScene("MiniGameScene");
                break;
            case InteractType.MissionComplete:
                break;
            default:
                break;
        }

        OffMessageUI();
    }
    private void OnMessageUI(string msg)
    {
        GameMessageUI.GetComponentInChildren<TextMeshProUGUI>().text = msg;
        GameMessageUI.SetActive(true);
    }
    public void OffMessageUI()
    {
        CanGetBoxMission = false;
        GameMessageUI.SetActive(false);
    }
    #endregion

    #region 박스 미션
    /// <summary>
    /// 창고 물건 진열하는 미션 시작 행동 ('박스미션' 으로 통칭)
    /// </summary>
    private void BoxMissionStart()
    {
        IsWorking = true;
        // 상자 무게에 따라 이동 속도 감소
        boxWeight = Random.Range(0f, 4f);
        resourceController.MoveSpeed -= boxWeight;
        // 상자 들고있게 변경
        animationHandler.SwitchHolding(true);
        animationHandler.HeadFoodOn();
        // 내려둘 곳 표시
        CompleteZone.ZoneParticle.Play();
        BoxMissionZone.ZoneParticle.Stop();
    }

    /// <summary>
    /// 박스미션 완료 행동
    /// </summary>
    public void BoxMissionComplete()
    {
        IsWorking = false;

        animationHandler.SwitchHolding(false);
        animationHandler.HeadFoodOff();
        // 미션 완료 보상
        resourceController.MoveSpeed += boxWeight;
        int rewardCoin = (int)(Random.Range(0, boxWeight) * 10);
        resourceController.AddCoin(rewardCoin);
        // 표시
        CompleteZone.ZoneParticle.Stop();
        BoxMissionZone.ZoneParticle.Play();
    }
    #endregion
}
