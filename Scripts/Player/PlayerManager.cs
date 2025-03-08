using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;

    private static PlayerManager instance;
    
    public static PlayerManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
}
