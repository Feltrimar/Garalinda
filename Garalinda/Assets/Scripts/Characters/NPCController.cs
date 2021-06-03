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
  public Character character;
  bool able;
  public PlayerPrefs player;
  public GameObject tile;
  [SerializeField] Dialog dialogAuxYes;
  [SerializeField] Dialog dialogAuxNo;
  [SerializeField] Dialog dialogAuxShop;
  [SerializeField] public List<ItemBase> items;


  public List<Character> characters;
    private void Awake(){
        characters.AddRange(player.battleCharacters);
        characters.AddRange(player.reserveCharacters);
    }
  public void Interact()
  {
      DialogManager.Instance.ShowDialog(dialog, skill, shop, this);
      
  }

  public void Skill()
  {
    if(characters.Contains(character)){  
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
}
