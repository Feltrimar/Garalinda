using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public event Action OnBattleOver;
    public static PlayerPrefs pp;
    public GameObject chrimg;
    public List<Image> playableImages;

    private void OnEnable(){
        pp=GameObject.Find("GlobalVariable").GetComponent<PlayerPrefs>();
        SetupBattle();
    }

    public void SetupBattle(){
        chrimg.SetActive(true);
        PositionPlayableCharacters();
        PositionEnemyCharacters();
    }
    public void HandleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.X))
            OnBattleOver();
    }

    private void PositionPlayableCharacters(){
        for(int i=0;i<playableImages.Count;i++){
        playableImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(new Vector3(10,10,10));
        }
        for(int i=0;i<pp.battleCharacters.Count;i++){
                playableImages[i].sprite=pp.battleCharacters[i]._base.Image;
                playableImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(pp.battleCharacters[i].node.transform.position);
              
        }
    }

    private void PositionEnemyCharacters(){
       /* for(int i=0;i<playableImages.Count;i++){
        playableImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(new Vector3(10,10,10));
        }
        for(int i=0;i<pp.battleCharacters.Count;i++){
                playableImages[i].sprite=pp.battleCharacters[i]._base.Image;
                playableImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(pp.battleCharacters[i].node.transform.position);
              */
        }
   


}
