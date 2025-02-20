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
    // LayerMask�� ���� Layer Ȯ�� ����!!!
    [SerializeField] private LayerMask canInteractTargetLayers;

    public ParticleSystem ZoneParticle;
    public InteractType interactType;

    WorkManager workManager;

    private void Start()
    {
        workManager = FindObjectOfType<WorkManager>();
        ZoneParticle = GetComponentInChildren<ParticleSystem>();
        // ���ͷ��� �� ǥ�� 
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
        // ��ȣ�ۿ� ������Ʈ : canInteractTargetLayers�� ���ԵǴ� ������ Ȯ��
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
                    // �ʿ��ϴٸ� �̼� ���� Ȯ��
                    if (workManager.IsWorking && WorkManager.Instance.NPCWorking == false)
                    {
                        workManager.BoxMissionComplete();
                    }
                    break;
                case InteractType.CustomerRequest:
                    // �� ��û �̺�Ʈ ���� ��
                    GameManager.Instance.CustomerEventInProgress();
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
                case InteractType.MiniGame:
                    workManager.OffMessageUI();
                    break;
                default:
                    break;
            }
        }
    }
}
