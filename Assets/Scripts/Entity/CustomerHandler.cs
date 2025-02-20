using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum ERequestState
{
    EnterCustomer,  // 요청 전
    FindFood,       // 요청 수락 후 물건 찾기
    Delivery,       // 고객에게 전달
    Complete,       // 수행 완료
}
public class CustomerHandler : MonoBehaviour
{
    public GameObject RequestPrefab;

    public ERequestState CurState { get; set; }

    private GameObject RequestZone;
    private RandomFood randomFood;

    
    private void Start()
    {
        randomFood = GetComponentInChildren<RandomFood>(true);
        CurState = ERequestState.EnterCustomer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") == false) return;

        switch (CurState)
        {
            case ERequestState.EnterCustomer:
                // 요청 띄우기 & 생성 & 상태 변경
                Debug.Log("고객 입장");
                randomFood.RandomOn();
                InitRequestZone();
                CurState = ERequestState.FindFood;
                break;
            case ERequestState.Delivery:
                Debug.Log("운반 완료 끝!");
                randomFood.SpriteColorOn();
                // 완료
                CurState = ERequestState.Complete;
                Destroy(RequestZone, 1f);
                Destroy(gameObject, 1f);
                // 보상
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
