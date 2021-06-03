using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] PlayerPrefs pp;

public event Action onBack;

    int selectedItem = 0;

    public List<Text> menuItems;
    public List<Character> characters;
    public List<ItemBase> items;
    public List<GameObject> nodes;
    public Character characterOnSelect;
    [SerializeField] Dialog dialogAuxYes;
    bool characterBool;
    bool itemBool;
    bool formationBool;
    bool nodeBool;
    bool prueba;
    private void Awake(){
        characters.AddRange(pp.battleCharacters);
        characters.AddRange(pp.reserveCharacters);
        menuItems = menu.GetComponentsInChildren<Text>().ToList();
    }
   public void OpenCSSMenu(bool charactersBool, bool itemsBool, bool formationsBool, bool nodesBool){
       characterBool=charactersBool;
       itemBool=itemsBool;
       formationBool=formationsBool;
       nodeBool=nodesBool;
       for (int i=0; i<menuItems.Count; i++){
            menuItems[i].text="Espacio vacío";
         }
       menu.SetActive(true);
       if(charactersBool||formationBool){
           for (int i=0; i<characters.Count; i++){
            menuItems[i].text=characters[i]._base.Name;
         }
         }else if(itemsBool){
            for (int i=0; i<pp.items.Count; i++){
            menuItems[i].text=pp.items[i].name;
         }
         }else if(nodeBool){
             for (int i=0; i<nodes.Count; i++){
            menuItems[i].text=nodes[i].name;
         }
        }
         
       UpdateItemSelection();
       }
   

   public void CloseCSSMenu(){
       menu.SetActive(false);
       onBack?.Invoke();
   }

     public void HandleUpdate(){
       int prevSelection = selectedItem;

       if(Input.GetKeyDown(KeyCode.DownArrow))
        ++selectedItem;
        if(Input.GetKeyDown(KeyCode.UpArrow))
        --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem,0,menuItems.Count - 1);
        if(prevSelection != selectedItem)
            UpdateItemSelection();

        if(Input.GetKeyDown(KeyCode.X)){
            
            if(nodeBool){
                if(!OccupiedNode(nodes[selectedItem])){
               characterOnSelect.node=nodes[selectedItem];
               if(selectedItem==9){
                   characterOnSelect.state=BattleState.Apoyo;
               }else{
                   characterOnSelect.state=BattleState.Principal;
                    }
                CloseCSSMenu();    
               }
               else{
                CloseCSSMenu();
                DialogManager.Instance.ShowDialog(dialogAuxYes, false, false, null);
               }
                }
            if(formationBool){
                characterOnSelect=characters[selectedItem];
                OpenCSSMenu(false,false,false,true);}

            
            
        }
        else if(Input.GetKeyDown(KeyCode.Return)){
            onBack?.Invoke();
            CloseCSSMenu();
            

        } 
   }

     void UpdateItemSelection(){
       for (int i=0; i<menuItems.Count; i++){
           if(i==selectedItem)
                menuItems[i].color = Color.red;       
            else
                menuItems[i].color = Color.black;
    }
   }
    bool OccupiedNode(GameObject node){
    prueba = false;
        for(int i=0; i<characters.Count; i++){
            if(characters[i].node==node){
                prueba=true;
                break;
            }
        }
    return prueba;
    }
}
