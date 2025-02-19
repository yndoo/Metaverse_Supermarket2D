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

    // �̼� �����ֱ�
    public void ShowMissionDesc()
    {
        if (hasMission) return; // �̹� ���� ���̸� X
        ShowGetMissionUI();
    }

    // �̼� �ֱ�
    public void MissionStart()
    {
        hasMission = true;
        OffMissionUI();

        // ���� ����ְ� ����

        // ������ �� ǥ��
    }

    // �̼� �Ϸ�
    public void CompleteMission()
    {
        hasMission = false;

        // ����, �״� UI Off ��.
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
