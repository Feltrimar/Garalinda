using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Dialog}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    public GameState state;

    private void Start(){
        playerMovement.OnBattle += StartBattle;
        battleSystem.OnBattleOver += EndBattle;

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
    
    void StartBattle(){
        state=GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
    }

    void EndBattle(){
        state=GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update(){
        if(state==GameState.FreeRoam){
            playerMovement.HandleUpdate();
        }
       else if(state==GameState.Battle){
          battleSystem.HandleUpdate();
       }
       else if(state==GameState.Dialog){
           DialogManager.Instance.HandleUpdate();
       }
    }
    
}
