using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int PlayerCoin { get; set; }
    public Vector3 LastPosition {  get; set; }

    ResourceController resourceController;
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
    void Start()
    {
        PlayerCoin = 0;
        LastPosition = Vector3.zero;
    }

    private void Update()
    {
        if(CustomerEventHandler == null)
            CustomerEventHandler = Instantiate(CustomerEventPrefab).GetComponent<CustomerHandler>();
    }

    public void CustomerEventInProgress()
    {
        if (CustomerEventHandler.CurState != ERequestState.FindFood) return;
        Debug.Log("���� ã����. ��� ��");
        CustomerEventHandler.CurState = ERequestState.Delivery;
        // �÷��̾� IsHolding
        animationHandler = FindObjectOfType<AnimationHandler>();
        animationHandler.SwitchHolding(true);
        animationHandler.HeadFoodOn(CustomerEventHandler.FoodNum);
    }
}
