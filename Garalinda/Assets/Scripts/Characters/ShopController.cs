using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance{get; private set;}
    List<ItemBase> soldItems;
    [SerializeField] GameObject shopBox;
    public event Action OnShowShop;
    public event Action onBack;
    // event Action OnCloseShop;
    int selectedItem = 0;
    List<Text> menuItems;
    public PlayerPrefs pp;
    [SerializeField] Dialog dialogAuxYes;
    [SerializeField] Dialog dialogAuxNo;
    [SerializeField] Dialog dialogAuxBye;
    void Awake(){
        
        Instance=this;
        menuItems = shopBox.GetComponentsInChildren<Text>().ToList();
        pp=GameObject.Find("GlobalVariable").GetComponent<PlayerPrefs>();

    }

    public void CloseShop(){
       shopBox.SetActive(false);
       
    }

    public void StartShop(NPCController npc){
        soldItems=npc.items;
        OnShowShop?.Invoke();
        shopBox.SetActive(true);
        for (int i=0; i<menuItems.Count; i++){
            menuItems[i].text=soldItems[i].Name+" - "+soldItems[i].Cost+" Oro";
        }
        UpdateItemSelection();
    }

    public void HandleUpdate()
    {   
    int prevSelection = selectedItem;

       if(Input.GetKeyDown(KeyCode.DownArrow))
        ++selectedItem;
        if(Input.GetKeyDown(KeyCode.UpArrow))
        --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem,0,menuItems.Count - 1);
        if(prevSelection != selectedItem)
            UpdateItemSelection();

        if(Input.GetKeyDown(KeyCode.X)){
            if(pp.gold >= soldItems[selectedItem].Cost){
                pp.gold=pp.gold-soldItems[selectedItem].Cost;
                pp.items.Add(soldItems[selectedItem]);
                onBack?.Invoke();
                CloseShop();
                DialogManager.Instance.ShowDialog(dialogAuxBye, false, false, null);
            }
            else{
               
                onBack?.Invoke();
                CloseShop();
                DialogManager.Instance.ShowDialog(dialogAuxNo, false, false, null);
            }
            
            
        }
        else if(Input.GetKeyDown(KeyCode.Return)){
            onBack?.Invoke();
            CloseShop();
            DialogManager.Instance.ShowDialog(dialogAuxBye, false, false, null);

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
}
