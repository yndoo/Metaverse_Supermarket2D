using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    // LayerMask로 여러 Layer 확인 가능!!!
    [SerializeField] private LayerMask canInteractTargetLayers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 상호작용 오브젝트 : canInteractTargetLayers에 포함되는 것인지 확인
        int isInsteract = canInteractTargetLayers.value | (1 << collision.gameObject.layer);
        if (canInteractTargetLayers.value == isInsteract)
        {
            MissionManager.instance.ShowMissionDesc();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 상호작용 오브젝트 : canInteractTargetLayers에 포함되는 것인지 확인
        int isInsteract = canInteractTargetLayers.value | (1 << collision.gameObject.layer);
        if (canInteractTargetLayers.value == isInsteract)
        {
            MissionManager.instance.OffMissionUI();
        }
    }
}
