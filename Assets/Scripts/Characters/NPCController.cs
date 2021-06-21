using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class NPCController : MonoBehaviour, Interactable
{
  [SerializeField] Dialog dialog;
  public bool skill;
  public bool shop;
  public CharacterBase character;
  bool able;
  public PlayerPrefs player;
  public GameObject tile;
  [SerializeField] Dialog dialogAuxYes;
  [SerializeField] Dialog dialogAuxNo;
  [SerializeField] Dialog dialogAuxShop;
  [SerializeField] public List<ItemBase> items;


  public List<Character> characters;
    private void Awake(){
        player=GameObject.Find("GlobalVariable").GetComponent<PlayerPrefs>();;
        characters.AddRange(player.battleCharacters);
        characters.AddRange(player.reserveCharacters);
    }
  public void Interact()
  {
      DialogManager.Instance.ShowDialog(dialog, skill, shop, this);
      
  }

  public void Skill()
  {
    if(CheckPower()){  
      DialogManager.Instance.ShowDialog(dialogAuxYes, false, false, this);
      tile.SetActive(false);
      }
    else
      DialogManager.Instance.ShowDialog(dialogAuxNo, false, false, this);

  }


  public void Shop()
  {
     // DialogManager.Instance.ShowDialog(dialogAuxShop, false, false, this);
      ShopController.Instance.StartShop(this);
  }

  public bool CheckPower(){
    able=false;
    for (int i=0; i<characters.Count; i++){
            if(characters[i]._base==character){
              able=true;
              break;}
        }
    return able;
  }
}
