using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    BoxMission,
    MissionComplete,
    MiniGame,
}

public class InteractZone : MonoBehaviour
{
    // LayerMask�� ���� Layer Ȯ�� ����!!!
    [SerializeField] private LayerMask canInteractTargetLayers;

    public InteractType interactType;

    MissionManager missionManager;

    private void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ȣ�ۿ� ������Ʈ : canInteractTargetLayers�� ���ԵǴ� ������ Ȯ��
        int isInsteract = canInteractTargetLayers.value | (1 << collision.gameObject.layer);
        if (canInteractTargetLayers.value == isInsteract)
        {
            switch(interactType)
            {
                case InteractType.BoxMission:
                    if (!missionManager.DoingMission())
                    {
                        missionManager.ShowMissionDesc();
                    }
                    break;
                case InteractType.MissionComplete:
                    // �ʿ��ϴٸ� �̼� ���� Ȯ��
                    if (missionManager.DoingMission())
                    {
                        missionManager.CompleteMission();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ��ȣ�ۿ� ������Ʈ : canInteractTargetLayers�� ���ԵǴ� ������ Ȯ��
        int isInsteract = canInteractTargetLayers.value | (1 << collision.gameObject.layer);
        if (canInteractTargetLayers.value == isInsteract)
        {
            switch (interactType)
            {
                case InteractType.BoxMission:
                    missionManager.OffMissionUI();
                    break;
                default:
                    break;
            }
        }
    }
}
