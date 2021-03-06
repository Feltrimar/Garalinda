using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text text;
    [SerializeField] int lettersPerSecond;
    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public static DialogManager Instance{get; private set;}
    bool skillAux;
    bool shopAux;
    NPCController npcAux;
    private void Awake()
    {
        Instance=this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public void ShowDialog(Dialog dialog, bool skill, bool shop, NPCController npc){
        skillAux=skill;
        shopAux=shop;
        npcAux=npc;
        OnShowDialog?.Invoke();
        this.dialog=dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.X)&& !isTyping){
            ++currentLine;
            if(currentLine < dialog.Lines.Count){
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else{
                currentLine=0;
                dialogBox.SetActive(false);
           OnCloseDialog?.Invoke();
                if(skillAux)
                    npcAux.Skill();
                if(shopAux)
                    npcAux.Shop();

            skillAux=false;
            npcAux=null;
            shopAux=false;
            }
        }
    }
    public IEnumerator TypeDialog(string dialog){
        isTyping=true;
        text.text="";
        foreach (var letter in dialog.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
        isTyping=false;
    }
}
