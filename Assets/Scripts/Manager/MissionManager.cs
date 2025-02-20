using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject MissionMessageUI;

    public static MissionManager instance;

    public bool CanGetBoxMission { get; set; } // �ڽ� �̼� ���� �� �ִ� ���� Ȯ�� (�̼� �̼��� && ��ȣ�ۿ� ���¿��� ��.)

    private bool hasMission = false; // �̼� ���� ��.
    private InteractType curZone;
    private float boxWeight;

    public InteractController MissionZone;
    public InteractController CompleteZone;

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
    public void SetCurZone(InteractType zone)
    {
        this.curZone = zone;
    }

    #region �̼� UI ����
    public void ShowMissionDesc()
    {
        if (hasMission) return; // �̼��� ���� ���̸� X

        string msg = "";
        switch(curZone)
        {
            case InteractType.BoxMission:
                CanGetBoxMission = true;
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
        if (hasMission) return; // �̼��� ���� ���̸� X

        switch (curZone)
        {
            case InteractType.BoxMission:
                if (CanGetBoxMission)
                {
                    BoxMissionStart();
                    CanGetBoxMission = false;
                    hasMission = true;
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

        OffMissionUI();
    }
    private void ShowGetMissionUI(string msg)
    {
        MissionMessageUI.GetComponentInChildren<TextMeshProUGUI>().text = msg;
        MissionMessageUI.SetActive(true);
    }
    public void OffMissionUI()
    {
        CanGetBoxMission = false;
        MissionMessageUI.SetActive(false);
    }
    #endregion

    /// <summary>
    /// â�� ���� �����ϴ� �̼� ���� �ൿ ('�ڽ��̼�' ���� ��Ī)
    /// </summary>
    private void BoxMissionStart()
    {
        // ���� ���Կ� ���� �̵� �ӵ� ����
        boxWeight = Random.Range(0f, 4f);
        resourceController.MoveSpeed -= boxWeight;
        // ���� ����ְ� ����
        animationHandler.SwitchHolding(true);
        if (randomBox == null)
        {
            randomBox = FindObjectOfType<RandomFood>(true);
        }
        randomBox.RandomOn();
        // ������ �� ǥ��
        CompleteZone.ZoneParticle.Play();
        MissionZone.ZoneParticle.Stop();
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
        // ǥ��
        CompleteZone.ZoneParticle.Stop();
        MissionZone.ZoneParticle.Play();
    }
}
