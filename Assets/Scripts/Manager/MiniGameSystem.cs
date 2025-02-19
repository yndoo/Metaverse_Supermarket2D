using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSystem : MonoBehaviour
{
    public static MiniGameSystem instance;
    public GameObject LifeUI;
    public GameObject RewardUI;
    public GameObject MinigameMessageUI;
    public TextMeshProUGUI TimeTxt;

    public bool IsRunning {  get => isRunning; private set { IsRunning = value; } }

    private TextMeshProUGUI BestRecordTxt;
    private TextMeshProUGUI TimeResultTxt;
    private TextMeshProUGUI CoinResultTxt;

    private const int LifeCount = 3;
    private const string bestRecordKey = "BestScore";

    private int index = 2;
    private float curTime = 0f;
    private bool isRunning = false;

    private float uiTime = 2f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        MinigameMessageUI.GetComponentInChildren<TextMeshProUGUI>(true).text = "���� ��Ƴ�������.";
        MinigameMessageUI.SetActive(true);
        Invoke("MsgOff", 3f);

        isRunning = true;
        index = LifeCount - 1;

        TextMeshProUGUI[] rewards = RewardUI.GetComponentsInChildren<TextMeshProUGUI>(true);
        TimeResultTxt = rewards[0];
        CoinResultTxt = rewards[1];
        BestRecordTxt = rewards[2];
    }

    private void Update()
    {
        if(isRunning)
        {
            curTime += Time.deltaTime;
            TimeTxt.text = curTime.ToString("N2");
            return;
        }
    }
    /// <summary>
    /// ������ ��� �Լ�
    /// </summary>
    public void OffLifeUI()
    {
        if (!isRunning) return;

        LifeUI.transform.GetChild(index--).gameObject.SetActive(false);

        if(index < 0)
        {
            // ���� ��
            isRunning = false;
            // ��� ���
            float best = PlayerPrefs.GetFloat(bestRecordKey);
            if(best < curTime)
            {
                PlayerPrefs.SetFloat(bestRecordKey, curTime);
                best = curTime;
            }
            BestRecordTxt.text = $"Best Record {best.ToString("N2")}";
            TimeResultTxt.text = curTime.ToString("N2");

            // ���� ���
            float coinP = Random.Range(5f, 100f);
            int rewardCoin = (int)Mathf.Ceil(curTime * coinP);
            GameManager.Instance.PlayerCoin += rewardCoin;
            CoinResultTxt.text = rewardCoin.ToString();
            
            // ����UI Ȱ��ȭ
            RewardUI.SetActive(true);
            Invoke("TimeResultActive", 1f);
            Invoke("CoinResultActive", 2f);
            Invoke("ExitMessageActive", 3f);
        }
    }

    private void TimeResultActive()
    {
        
        TimeResultTxt.gameObject.SetActive(true);
    }
    private void CoinResultActive()
    {
        CoinResultTxt.gameObject.SetActive(true);
    }
    private void ExitMessageActive()
    {
        MinigameMessageUI.GetComponentInChildren<TextMeshProUGUI>(true).text = "Press \'E\'";
        MinigameMessageUI.SetActive(true);
    }
    private void MsgOff()
    {
        MinigameMessageUI.SetActive(false);
    }
    public void MiniGameExit()
    {
        isRunning = false;
        SceneManager.LoadScene("MainScene");
    }
}
