using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NodeWorldMap : MonoBehaviour
{
  public NodeWorldMap nodeUp;
  public NodeWorldMap nodeDown;
  public NodeWorldMap nodeRight;
  public NodeWorldMap nodeLeft;
  public NodeWorldMap Instance { get; private set;}
  public string _name;
  public GameObject player;
  public GameObject playerAux;
  public int sceneToLoad;
  public Text canvasText;
void Awake(){
    Instance = this;
}


void Update(){
    nodeMovement();
     if(Input.GetKeyDown(KeyCode.X)){
            if(player!=null){
       SceneManager.LoadSceneAsync(sceneToLoad);
       
                }
            }   
        }
    
void nodeMovement(){
    if (player!=null){
        if(nodeUp != null)
    {
       if(Input.GetKeyDown(KeyCode.UpArrow)){ 
        player.transform.position=nodeUp.transform.position;
        nodeUp.player = player;
        player=null;
        canvasText.text=nodeUp._name;
}
        
    }
    if(nodeDown != null){
       if(Input.GetKeyDown(KeyCode.DownArrow)){ 
        player.transform.position=nodeDown.transform.position;
        nodeDown.player = player;
        player=null;
        canvasText.text=nodeDown._name;}
    }
    if(nodeLeft != null)
    {
       if(Input.GetKeyDown(KeyCode.LeftArrow)){ 
        this.player.transform.position=nodeLeft.transform.position;
        nodeLeft.player = player;
        player=null;
        canvasText.text=nodeLeft._name;}
    }
    if(nodeRight != null)
    {
       if(Input.GetKeyDown(KeyCode.RightArrow)){ 
        this.player.transform.position=nodeRight.transform.position;
        nodeRight.player = player;
        player=null;
        canvasText.text=nodeRight._name;}
        }
    }


}
}