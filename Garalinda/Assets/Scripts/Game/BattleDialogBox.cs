using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    bool isTyping;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject targetSelector;
    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;
    [SerializeField] List<Text> targetTexts;

    [SerializeField] GameObject formationSelector;
    [SerializeField] List<Text> formationTexts;
    [SerializeField] GameObject itemSelector;
    [SerializeField] List<Text> itemTexts;
    public List<ItemBase> usableItems;
    public void SetDialog(string dialog)
    {
        dialogText.text=dialog;
    }

    
     public IEnumerator TypeDialog(string dialog){
        dialogText.text="";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/30);
        }
    }

    public void EnableDialogText(bool enabled){
        dialogText.enabled = enabled;
    }

    public void EnableActionSelection(bool enabled){
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelection(bool enabled){
        moveSelector.SetActive(enabled);
    }

    public void EnableTargetSelection(bool enabled){
        targetSelector.SetActive(enabled);
    }

     public void EnableFormationSelection(bool enabled){
        formationSelector.SetActive(enabled);
    }

     public void EnableItemSelection(bool enabled){
        itemSelector.SetActive(enabled);
    }

    public void UpdateActionSelection(int currentSelection){
        for(int i=0; i<actionTexts.Count;++i){
            if(i==currentSelection)
                actionTexts[i].color=Color.red;
            else
            {
                actionTexts[i].color=Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int currentMove){
        for(int i=0; i<moveTexts.Count;++i){
            if(i==currentMove)
                moveTexts[i].color=Color.red;
            else
            {
                moveTexts[i].color=Color.black;
            }
        }
    }

    public void SetAbilityNames(List<Ability> abilities){
        for(int i=0; i<moveTexts.Count;++i){
            if(i<abilities.Count)
                moveTexts[i].text=abilities[i]._base.Name;
            else
            {
                moveTexts[i].text="Sin habilidad";
            }
        }
    }

    public void UpdateTargetSelection(int currentTarget){
        for(int i=0; i<targetTexts.Count;++i){
            if(i==currentTarget)
                targetTexts[i].color=Color.red;
            else
            {
                targetTexts[i].color=Color.black;
            }
        }
    }

    public void SetTargetNames(List<Character> characters){
        for(int i=0; i<targetTexts.Count;++i){
            if(i<characters.Count)
                targetTexts[i].text=characters[i]._base.Name;
            else
            {
                targetTexts[i].text="-";
            }
        }
    }

    public void SetTargetNames(List<EnemyUnit> characters){
        for(int i=0; i<targetTexts.Count;++i){
            if(i<characters.Count)
                targetTexts[i].text=characters[i].GetComponent<Character>()._base.Name;
            else
            {
                targetTexts[i].text="-";
            }
        }
    }

    public void SetItemNames(List<ItemBase> items){
        usableItems=prepareItems(items);
        for(int i=0; i<itemTexts.Count;++i){
            if(i<usableItems.Count)
                itemTexts[i].text=usableItems[i].Name;
            else
            {
                itemTexts[i].text="-";
            }
        }
    }

    public List<ItemBase> prepareItems(List<ItemBase> items){
        usableItems.Clear();
        for(int i=0;i<items.Count;i++){
            if(items[i].ItemType==ItemType.Usable)
                usableItems.Add(items[i]);
        }
        return usableItems;
    }

    public void UpdateItemSelection(int currentItem){
        for(int i=0; i<itemTexts.Count;++i){
            if(i==currentItem)
                itemTexts[i].color=Color.red;
            else
            {
                itemTexts[i].color=Color.black;
            }
        }
    }

    public void UpdateFormationSelection(int currentFormation){
        for(int i=0; i<formationTexts.Count;++i){
            if(i==currentFormation)
                formationTexts[i].color=Color.red;
            else
            {
                formationTexts[i].color=Color.black;
            }
        }
    }

    public void SetFormationNames(List<GameObject> formations){
        for(int i=0; i<formationTexts.Count;++i){
            if(i<formations.Count)
                formationTexts[i].text=formations[i].name;
            else
            {
                formationTexts[i].text="-";
            }
        }
    }
}
