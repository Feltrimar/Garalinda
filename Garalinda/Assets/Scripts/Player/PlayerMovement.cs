using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask portalLayer;
    public bool stageWithEncounters;
    public float battleCount;
    public bool movNotAllowed;
    private bool isMoving;
    private Vector2 input;
    public Text alertaText;

    Color _orange = new Color(1.0f, 0.64f, 0.0f);
    void Start()
    {
        
    }

    public void HandleUpdate()
    {
        if(!isMoving){
            input.x=Input.GetAxisRaw("Horizontal");
            input.y=Input.GetAxisRaw("Vertical");

            if(input != Vector2.zero)
            {

                if(input.x !=0) input.y = 0;
                var targetPos =transform.position;
                targetPos.x +=input.x;
                targetPos.y +=input.y;
                if(IsWalkable(targetPos)&& !movNotAllowed)
                    StartCoroutine((Move(targetPos)));

            }
        }
        if(!movNotAllowed)
            Alerta();
        CheckForPortals();
        if(Input.GetKeyDown(KeyCode.X))
            Interact();
    
    }
    void Interact(){
        var collider = Physics2D.OverlapCircle(transform.position,0.3f, interactableLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
        
    }

    IEnumerator Move(Vector3 targetPos){
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position,targetPos,speed *Time.deltaTime);
            yield return null;
        
        
        }
        transform.position = targetPos;
        isMoving = false;
        CheckForEncounters();
       // CheckForPortals();

    }

    private bool IsWalkable(Vector3 targetPos){
        if(Physics2D.OverlapCircle(targetPos,0.2f,solidObjectsLayer | interactableLayer) !=null){
            return false;
        }
        return true;
    }

    private void CheckForEncounters(){
        if(stageWithEncounters)
        {
            if(UnityEngine.Random.Range(1,101)<=10)
            {
                if(battleCount<3){
                    battleCount++;
                Debug.Log("CHECK FOR ENCOUNTER");
                }
                else{
                Debug.Log("BATTLE ENCOUNTER");
                battleCount=0;
                }
            }
        }
    }

  private void CheckForPortals(){
      var targetPos =transform.position;
      var collider = Physics2D.OverlapCircle(targetPos,0.2f,portalLayer);
        if(collider != null){
            var trigger  = collider.GetComponent<IPlayerTriggerable>();
            trigger.OnPlayerTriggered(this);
        }

  }

  void Alerta(){
        float alerta = battleCount;
        if(alerta==0){
            alertaText.text="Seguro";
            alertaText.color=Color.white;

        }
        else if(alerta==1){
            alertaText.text="Cuidado";
            alertaText.color=Color.yellow;
        }
        else if(alerta==2){
            alertaText.text="Alerta";
            alertaText.color=_orange;
        }
        else if(alerta==3){
            alertaText.text="Peligro";
            alertaText.color=Color.red;
        }
    }
}
