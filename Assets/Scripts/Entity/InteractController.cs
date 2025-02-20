using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    BoxMission,
    MissionComplete,
    MiniGame,
    CustomerRequest,
}

public class InteractController : MonoBehaviour
{
    // LayerMask로 여러 Layer 확인 가능!!!
    [SerializeField] private LayerMask canInteractTargetLayers;

    public ParticleSystem ZoneParticle;
    public InteractType interactType;

    WorkManager workManager;

    private void Start()
    {
        workManager = FindObjectOfType<WorkManager>();
        ZoneParticle = GetComponentInChildren<ParticleSystem>();
        // 인터랙션 존 표시 
        switch (interactType)
        { 
            case InteractType.BoxMission:
                workManager.BoxMissionZone = this;
                ZoneParticle.Play();
                break;
                case InteractType.MissionComplete:
                workManager.CompleteZone = this;
                ZoneParticle.Stop();
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 상호작용 오브젝트 : canInteractTargetLayers에 포함되는 것인지 확인
        int isInsteract = canInteractTargetLayers.value | (1 << collision.gameObject.layer);
        if (canInteractTargetLayers.value == isInsteract)
        {
            workManager.CurZone = interactType;

            switch (interactType)
            {
                case InteractType.BoxMission:
                case InteractType.MiniGame:
                    if (workManager.IsWorking == false && WorkManager.Instance.NPCWorking == false)
                    {
                        workManager.ShowMissionDesc();
                    }
                    break;
                case InteractType.MissionComplete:
                    // 필요하다면 미션 조건 확인
                    if (workManager.IsWorking && WorkManager.Instance.NPCWorking == false)
                    {
                        workManager.BoxMissionComplete();
                    }
                    break;
                case InteractType.CustomerRequest:
                    // 고객 요청 이벤트 진행 중
                    GameManager.Instance.CustomerEventInProgress();
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
                case InteractType.MiniGame:
                    workManager.OffMessageUI();
                    break;
                default:
                    break;
            }
        }
    }
}
