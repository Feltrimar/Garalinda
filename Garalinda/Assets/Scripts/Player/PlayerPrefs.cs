using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefs : MonoBehaviour
{
    public float gold;
    public List<Character> battleCharacters;
    public List<Character> reserveCharacters;
    public Text goldText;
    public List<ItemBase> items;

    void Update(){
        goldText.text=gold.ToString();
        
    }


    
}
