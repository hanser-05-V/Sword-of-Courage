using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
   void OnEnter(PlayerInfo playerInfo,PlayerStats playerStats);

   void Onupdate(PlayerInfo playerInfo,PlayerStats playerStats);

   void OnExit(PlayerInfo playerInfo,PlayerStats playerStats);
}
