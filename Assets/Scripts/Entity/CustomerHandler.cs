using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


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

    private GameObject RequestZone;
    private RandomFood randomFood;

    
    private void Start()
    {
        randomFood = GetComponentInChildren<RandomFood>();
        CurState = ERequestState.EnterCustomer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") == false) return;

        switch (CurState)
        {
            case ERequestState.EnterCustomer:
                // ��û ���� & ���� & ���� ����
                Debug.Log("�� ����");
                randomFood.RandomOn();
                RequestZone = Instantiate(RequestPrefab);
                CurState = ERequestState.FindFood;
                break;
            case ERequestState.Delivery:
                Debug.Log("��� �Ϸ� ��!");
                // �Ϸ�
                CurState = ERequestState.Complete;
                Destroy(gameObject);
                // ����
                break;
            default:
                break;
        }
    }
}
