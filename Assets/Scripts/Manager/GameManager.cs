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

    // 게임 이벤트 스포너 역할을 해야 함...
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
        Debug.Log("물건 찾았음. 운반 중");
        CustomerEventHandler.CurState = ERequestState.Delivery;
        // 플레이어 IsHolding
        animationHandler = FindObjectOfType<AnimationHandler>();
        animationHandler.SwitchHolding(true);
        animationHandler.HeadFoodOn(CustomerEventHandler.FoodNum);
    }
}
