using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class PlayerPrefs : MonoBehaviour
{
    public float gold;
    public List<Character> battleCharacters;
    public List<Character> reserveCharacters;
    public List<ItemBase> items;
    public List<Character> characters;
    public List<Character> battleCharactersAux;
    public List<Character> reserveCharactersAux;

    void Awake(){
    GetOrderedCharacters();
    }

    void Update(){
        
        
    }

    public void GetOrderedCharacters(){
        characters=gameObject.GetComponentsInChildren<Character>().ToList();
        battleCharactersAux.Clear();
        reserveCharactersAux.Clear();
        for(int i=0; i<characters.Count;i++){
            if(characters[i].node==null){
                reserveCharactersAux.Add(characters[i]);
            }
            else{
                battleCharactersAux.Add(characters[i]);
            }
        }
        battleCharacters=battleCharactersAux;
        reserveCharacters=reserveCharactersAux;
        
    }

    
}
