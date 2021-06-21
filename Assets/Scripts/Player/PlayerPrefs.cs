using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public List<GameObject> nodes;
    public List<Transform> nodes2;
    public int i;
    public int j;

    void Awake(){
    GetOrderedCharacters();
    }

    void Update(){
        int y = SceneManager.GetActiveScene().buildIndex;
        if(y!=1&&y!=6&&y!=0&&(nodes.Count==0||nodes[1]==null)){
            nodes.Clear();
            nodes2=GameObject.Find("Nodes").GetComponentsInChildren<Transform>().ToList();
            for(int i=1; i<nodes2.Count;i++){
                nodes.Add(nodes2[i].gameObject);
            }
        }

        System.Random rnd = new System.Random();
        if(battleCharacters.Count()==0){
        i=rnd.Next(0,reserveCharacters.Count());
        j=rnd.Next(1,10);
        reserveCharacters[i].node=nodes[j];
        }
        GetOrderedCharacters();

        
    }

    public void GetOrderedCharacters(){
        characters=gameObject.GetComponentsInChildren<Character>().ToList();
        for(int i=0; i<characters.Count(); i++)
            characters[i].Setup(characters[i]._base,characters[i].level);
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
