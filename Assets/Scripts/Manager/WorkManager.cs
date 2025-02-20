using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WorkManager : MonoBehaviour
{
    [SerializeField] private GameObject GameMessageUI;

    public static WorkManager Instance { get; private set; }

    public bool CanGetBoxMission { get; set; } // �ڽ� �̼� ���� �� �ִ� ���� Ȯ�� (�ڽ� �̼� �̼��� && ��ȣ�ۿ� ���¿��� ��.)
    public bool IsWorking { get; set; } // � ������ ���ϴ� ��.
    public bool NPCWorking { get; set; } // NPC ����Ʈ ��� ��(�ٸ� �� ���ϴ� ����)
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

    #region �̼� UI ����
    public void ShowMissionDesc()
    {
        if (IsWorking) return; // �̼��� ���� ���̸� X

        string msg = "";
        switch(CurZone)
        {
            case InteractType.BoxMission:
                CanGetBoxMission = true;
                msg = "������ �����ؾ� �մϴ�.\n�����Ͻðڽ��ϱ�?";
                break;
            case InteractType.MiniGame:
                msg = "���� �մ����κ��� ������ �� �ֽ��ϴ�.";
                break;
        }
        OnMessageUI(msg);
    }
    /// <summary>
    /// �̼� ���� �� ����
    /// </summary>
    public void MissionStart()
    {
        if (IsWorking || NPCWorking) return; // �̼��� ���� ���̸� X

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
                // �̴ϰ��� ���� 
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

    #region �ڽ� �̼�
    /// <summary>
    /// â�� ���� �����ϴ� �̼� ���� �ൿ ('�ڽ��̼�' ���� ��Ī)
    /// </summary>
    private void BoxMissionStart()
    {
        IsWorking = true;
        // ���� ���Կ� ���� �̵� �ӵ� ����
        boxWeight = Random.Range(0f, 4f);
        resourceController.MoveSpeed -= boxWeight;
        // ���� ����ְ� ����
        animationHandler.SwitchHolding(true);
        animationHandler.HeadFoodOn();
        // ������ �� ǥ��
        CompleteZone.ZoneParticle.Play();
        BoxMissionZone.ZoneParticle.Stop();
    }

    /// <summary>
    /// �ڽ��̼� �Ϸ� �ൿ
    /// </summary>
    public void BoxMissionComplete()
    {
        IsWorking = false;

        animationHandler.SwitchHolding(false);
        animationHandler.HeadFoodOff();
        // �̼� �Ϸ� ����
        resourceController.MoveSpeed += boxWeight;
        int rewardCoin = (int)(Random.Range(0, boxWeight) * 10);
        resourceController.AddCoin(rewardCoin);
        // ǥ��
        CompleteZone.ZoneParticle.Stop();
        BoxMissionZone.ZoneParticle.Play();
    }
    #endregion
}
