using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    BoxMission,
    MissionComplete,
    MiniGame,
}

public class InteractController : MonoBehaviour
{
    // LayerMask�� ���� Layer Ȯ�� ����!!!
    [SerializeField] private LayerMask canInteractTargetLayers;

    public ParticleSystem ZoneParticle;
    public InteractType interactType;

    MissionManager missionManager;

    private void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
        ZoneParticle = GetComponentInChildren<ParticleSystem>();
        // ���ͷ��� �� ǥ�� 
        switch (interactType)
        { 
            case InteractType.BoxMission:
                missionManager.MissionZone = this;
                ZoneParticle.Play();
                break;
                case InteractType.MissionComplete:
                missionManager.CompleteZone = this;
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
            missionManager.SetCurZone(interactType);

            switch (interactType)
            {
                case InteractType.BoxMission:
                case InteractType.MiniGame:
                    if (!missionManager.DoingMission())
                    {
                        missionManager.ShowMissionDesc();
                    }
                    break;
                case InteractType.MissionComplete:
                    // �ʿ��ϴٸ� �̼� ���� Ȯ��
                    if (missionManager.DoingMission())
                    {
                        missionManager.BoxMissionComplete();
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
                case InteractType.MiniGame:
                    missionManager.OffMissionUI();
                    break;
                default:
                    break;
            }
        }
    }
}
