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

    #region ���ڹ̼� UI
    public void ShowMissionDesc()
    {
        if (hasMission) return; // �̹� ���� ���̸� X
        ShowGetMissionUI();
    }
    public void MissionStart()
    {
        hasMission = true;
        OffMissionUI();

        // ���� ����ְ� ����

        // ������ �� ǥ��
    }
    public void CompleteMission()
    {
        hasMission = false;
        Debug.Log("�̼� �Ϸ�");
        // ����, �״� UI Off ��.
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
