using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefs : MonoBehaviour
{
    public float gold;
    public List<CharacterBase> battleCharacters;
    public List<CharacterBase> reserveCharacters;
    public Text goldText;
    

    void Update(){
        goldText.text=gold.ToString();
        
    }


    
}
