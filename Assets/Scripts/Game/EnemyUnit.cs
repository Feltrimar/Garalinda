using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUnit : MonoBehaviour
{
    [SerializeField] public CharacterBase _base;
    [SerializeField] int level;
    public Character character;
    
    public void Setup(){
        if(gameObject.GetComponent<Character>()==null)
            character = gameObject.AddComponent<Character>();
        character.Setup(_base,level);
    }
}
