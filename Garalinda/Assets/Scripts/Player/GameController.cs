using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Dialog}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    //[SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    public GameState state;

    private void Start(){
        DialogManager.Instance.OnShowDialog+= () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialog+= () =>
        {
            if(state== GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }
    
    private void Update(){
        if(state==GameState.FreeRoam){
            playerMovement.HandleUpdate();
        }
   //     else if(state==GameState.Battle){
     //       battleSystem.HandleUpdate();
       // }
       else if(state==GameState.Dialog){
           DialogManager.Instance.HandleUpdate();
       }
    }
    
}
