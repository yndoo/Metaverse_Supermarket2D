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
    // LayerMask로 여러 Layer 확인 가능!!!
    [SerializeField] private LayerMask canInteractTargetLayers;

    public InteractType interactType;

    MissionManager missionManager;

    private void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 상호작용 오브젝트 : canInteractTargetLayers에 포함되는 것인지 확인
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
                    // 필요하다면 미션 조건 확인
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
        // 상호작용 오브젝트 : canInteractTargetLayers에 포함되는 것인지 확인
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
