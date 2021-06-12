using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScenes : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    public void OnTriggerEnter2D(Collider2D collision){
        StartCoroutine(SwitchScene());
    }


    public void OnPlayerTriggered(PlayerMovement player){
      StartCoroutine(SwitchScene());
    }

    
    IEnumerator SwitchScene(){
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
