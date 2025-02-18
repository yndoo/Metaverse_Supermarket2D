using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    private void Awake()
    {
        instance = this;
    }

    // 미션 보여주기
    public void ShowMissionDesc()
    {

    }

    // 미션 주기

    // 미션 완료
}
