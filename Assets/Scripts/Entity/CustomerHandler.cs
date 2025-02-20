using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;

/// <summary>
/// �� NPC �̺�Ʈ �߻� �ܰ�
/// </summary>
public enum ERequestState
{
    EnterCustomer,  // ��û ��
    FindFood,       // ��û ���� �� ���� ã��
    Delivery,       // ������ ����
    Complete,       // ���� �Ϸ�
}

public class CustomerHandler : MonoBehaviour
{
    public GameObject RequestPrefab;

    public ERequestState CurState { get; set; }
    public int FoodNum { get; set; }

    private GameObject RequestZone;
    private RandomFood customersRandomFood;

    AnimationHandler animationHandler;
    ResourceController resourceController;

    private void Awake()
    {
        animationHandler = FindObjectOfType<AnimationHandler>();
        resourceController = FindObjectOfType<ResourceController>();
        customersRandomFood = GetComponentInChildren<RandomFood>(true);
    }
    private void Start()
    {
        FoodNum = 0;
        CurState = ERequestState.EnterCustomer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") == false) return;
        if(WorkManager.Instance.IsWorking == true)
        {
            return;
        }

        switch (CurState)
        {
            case ERequestState.EnterCustomer:
                // ��û ���� & ���� & ���� ����
                Debug.Log("�� ����");
                WorkManager.Instance.NPCWorking = true;
                FoodNum = customersRandomFood.RandomOn();
                InitRequestZone();
                CurState = ERequestState.FindFood;
                break;
            case ERequestState.Delivery:
                Debug.Log("��� �Ϸ� ��!");
                // �Ϸ�
                CurState = ERequestState.Complete;
                WorkManager.Instance.NPCWorking = false;
                customersRandomFood.SpriteColorOn();
                animationHandler.SwitchHolding(false);
                animationHandler.HeadFoodOff();
                // ����
                resourceController.AddPopular(1);
                resourceController.AddCoin(10);
                // ����
                Destroy(RequestZone, 1f);
                Destroy(gameObject, 1f);
                break;
            default:
                break;
        }
    }

    private void InitRequestZone()
    {
        int p = Random.Range(0, 2);
        Vector3 pos = Vector3.zero;
        if(p == 0)
        {
            pos.x = Random.Range(1.5f, 4.5f);
            pos.y = 4f;
        }
        else
        {
            pos.x = Random.Range(-4.5f, -3.4f);
            pos.y = 7f;
        }

        RequestZone = Instantiate(RequestPrefab, pos, Quaternion.identity);
    }
}
