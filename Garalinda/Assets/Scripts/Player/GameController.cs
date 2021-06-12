using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Dialog, Menu, CSS, Shop, StageSelect}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    public GameState state;
    
    MenuController menuController;
    CharacterSelection characterSelection;
    ShopController shop;

    public static GameController Instance{ get; private set;}

    private void Awake(){
        Instance = this;
        menuController = GetComponent<MenuController>();
        characterSelection = GetComponent<CharacterSelection>();
    }

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

        ShopController.Instance.OnShowShop+= () =>
        {
            state = GameState.Shop;
        };

        ShopController.Instance.onBack += () => {
            state=GameState.FreeRoam;
        };

        menuController.onBack += () => {
            state=GameState.FreeRoam;
        };

        characterSelection.onBack += () => {
            state=GameState.FreeRoam;
        };

        menuController.onMenuSelected += OnMenuSelected;

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
        if(playerMovement==null){
            playerMovement=GameObject.Find("Player").GetComponent<PlayerMovement>();}


    
        if(state==GameState.FreeRoam){
            playerMovement.HandleUpdate();

            if(Input.GetKeyDown(KeyCode.Return)){
                menuController.OpenMenu();
                state = GameState.Menu;
            }
        }
       else if(state==GameState.Battle){
          battleSystem.HandleUpdate();
       }
       else if(state==GameState.Dialog){
           DialogManager.Instance.HandleUpdate();
       }
       else if (state == GameState.Menu){
           menuController.HandleUpdate();
       }
       else if (state == GameState.CSS){
           characterSelection.HandleUpdate();
       }
        else if (state == GameState.Shop){
           ShopController.Instance.HandleUpdate();
       }

    }
    
    void OnMenuSelected(int selectedItem){
        if(selectedItem==0){
            characterSelection.OpenCSSMenu(true,false,false,false);
            state=GameState.CSS;
        }
        if(selectedItem==1){
            characterSelection.OpenCSSMenu(false,true,false,false);
            state=GameState.CSS;
        }
        if(selectedItem==2){
            characterSelection.OpenCSSMenu(false,false,true,false);
            state=GameState.CSS;
        }
    }
}
