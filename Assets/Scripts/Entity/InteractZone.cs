using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    // LayerMask�� ���� Layer Ȯ�� ����!!!
    [SerializeField] private LayerMask canInteractTargetLayers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ȣ�ۿ� ������Ʈ : canInteractTargetLayers�� ���ԵǴ� ������ Ȯ��
        int isInsteract = canInteractTargetLayers.value | (1 << collision.gameObject.layer);
        if (canInteractTargetLayers.value == isInsteract)
        {
            MissionManager.instance.ShowMissionDesc();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ��ȣ�ۿ� ������Ʈ : canInteractTargetLayers�� ���ԵǴ� ������ Ȯ��
        int isInsteract = canInteractTargetLayers.value | (1 << collision.gameObject.layer);
        if (canInteractTargetLayers.value == isInsteract)
        {
            MissionManager.instance.OffMissionUI();
        }
    }
}
