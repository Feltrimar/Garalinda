using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public event Action OnBattleOver;
    
    public void HandleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.X))
            OnBattleOver();
    }


}
