using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Vector3 LastPosition {  get; set; }

    AnimationHandler animationHandler;

    // ���� �̺�Ʈ ������ ������ �ؾ� ��...
    public GameObject CustomerEventPrefab;
    public CustomerHandler CustomerEventHandler {  get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetCustomerHandler(CustomerHandler ch)
    {
        CustomerEventHandler = ch;
    }
    void Start()
    {
        LastPosition = Vector3.zero;
        InvokeRepeating("RandomCustomerEvent", 1f, 10f);
    }

    public void CustomerEventInProgress()
    {
        if (CustomerEventHandler.CurState != ERequestState.FindFood) return;
        Debug.Log("���� ã����. ��� ��");
        CustomerEventHandler.CurState = ERequestState.Delivery;
        WorkManager.Instance.NPCWorking = true;

        // �÷��̾� IsHolding
        animationHandler = FindObjectOfType<AnimationHandler>();
        animationHandler.SwitchHolding(true);
        animationHandler.HeadFoodOn(CustomerEventHandler.FoodNum);
    }

    public void RandomCustomerEvent()
    {
        if (WorkManager.Instance.IsWorking == true || WorkManager.Instance.IsNPCExist == true) return;
        if (CustomerEventHandler != null) return;

        int p = Random.Range(0, 101);
        Debug.Log(p);
        if (p < ResourceManager.Instance.PlayerPopular + 50)
        {
            CustomerEventHandler = Instantiate(CustomerEventPrefab).GetComponent<CustomerHandler>();
            WorkManager.Instance.IsNPCExist = true;
        }
    }
}
