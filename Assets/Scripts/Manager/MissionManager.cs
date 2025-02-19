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

    #region �̼� UI ����
    public void ShowMissionDesc(InteractType type)
    {
        if (hasMission) return; // �̼��� ���� ���̸� X

        CanGetMission = true;
        curZone = type;

        string msg = "";
        switch(type)
        {
            case InteractType.BoxMission:
                msg = "������ �����ؾ� �մϴ�.\n�����Ͻðڽ��ϱ�?";
                break;
            case InteractType.MiniGame:
                msg = "���� �մ����κ��� ������ �� �ֽ��ϴ�.";
                break;
        }
        ShowGetMissionUI(msg);
    }
    /// <summary>
    /// �̼� ���� �� ����
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
                // �̴ϰ��� ���� 
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
    /// â�� ���� �����ϴ� �̼� ���� �ൿ ('�ڽ��̼�' ���� ��Ī)
    /// </summary>
    private void BoxMissionStart()
    {
        hasMission = true;
        // ���� ���Կ� ���� �̵� �ӵ� ����
        boxWeight = Random.Range(0f, 4f);
        resourceController.MoveSpeed -= boxWeight;
        // ���� ����ְ� ����
        animationHandler.SwitchHolding(true);
        if (randomBox == null)
        {
            randomBox = FindObjectOfType<RandomFood>(true);
            //randomBox.gameObject.SetActive(true);
        }
        randomBox.RandomOn();
        // ������ �� ǥ��
    }
    /// <summary>
    /// �ڽ��̼� �Ϸ� �ൿ
    /// </summary>
    public void BoxMissionComplete()
    {
        hasMission = false;
        animationHandler.SwitchHolding(false);
        randomBox.gameObject.SetActive(false);
        // �̼� �Ϸ� ����
        resourceController.MoveSpeed += boxWeight;
        int rewardCoin = (int)(Random.Range(0, boxWeight) * 10);
        resourceController.AddCoin(rewardCoin);
    }
}
