using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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

    #region �̼� UI
    public void ShowMissionDesc(InteractType type)
    {
        if (hasMission) return; // �̼��� ���� ���̸� X

        curZone = type;

        string msg = "";
        switch(type)
        {
            case InteractType.BoxMission:
                msg = "Press 'E' to Get Mission";
                break;
            case InteractType.MiniGame:
                msg = "Press 'E' to Enter the MiniGame";
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
                // ���� ����ְ� ����
                // ������ �� ǥ��
                break;
            case InteractType.MiniGame:
                // �̴ϰ��� ����
                break;
            default:
                break;
        }

    }
    public void CompleteMission()
    {
        hasMission = false;
        Debug.Log("�̼� �Ϸ�");

        // ����, �״� UI Off ��.
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
