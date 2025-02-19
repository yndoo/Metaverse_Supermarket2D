using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int PlayerCoin { get; set; }
    public Vector3 LastPosition {  get; set; }

    ResourceController resourceController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        PlayerCoin = 0;
        LastPosition = Vector3.zero;
    }
}
