using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterBase _base;
    public int level;
    public int experience;

    public int HP {get; set;}
    
    public List<Ability> MainAbilities {get; set;}
    public List<Ability> SupportAbilities {get; set;}
    public GameObject node;
    public BattleState state;
    public Character(CharacterBase chbase, int chLevel){

        _base=chbase;
        level=chLevel;
        HP = _base.MaxHP;

        
        //Asignar habilidades con el nivel
        MainAbilities= new List<Ability>();
        foreach(var ability in _base.LearnableAbilitiesMain){
            if (ability.Level<=level)
                MainAbilities.Add(new Ability(ability.Base));
        }

        SupportAbilities= new List<Ability>();
        foreach(var ability in _base.LearnableAbilitiesSupport){
            if (ability.Level<=level)
                SupportAbilities.Add(new Ability(ability.Base));
        }

    }
    
     public int MaxHP {
        get { return _base.MaxHP + Mathf.FloorToInt(((_base.MaxHP/100f)*level));}
    }

    public int MaxEnergy {
        get { return _base.MaxEnergy + Mathf.FloorToInt(((_base.MaxEnergy/100f)*level));}
    }

    public int Attack {
        get { return _base.Attack + _base.Weapon.Attack + _base.Accessory1.Attack + _base.Accessory2.Attack + Mathf.FloorToInt(((_base.Attack/100f)*level));}
    }

    public int Defense {
        get { return _base.Defense + _base.Armor.Defense + _base.Accessory1.Defense + _base.Accessory2.Defense + Mathf.FloorToInt(((_base.Defense/100f)*level));}
    }
    
    public int MagicAttack {
        get { return _base.MagicAttack + _base.Weapon.MagicAttack + _base.Accessory1.MagicAttack + _base.Accessory2.MagicAttack + Mathf.FloorToInt(((_base.MagicAttack/100f)*level));}
    }

    public int MagicDefense {
        get { return _base.MagicDefense + _base.Armor.MagicDefense + _base.Accessory1.MagicDefense + _base.Accessory2.MagicDefense + Mathf.FloorToInt(((_base.MagicDefense/100f)*level));}
    }

    public int Speed {
        get { return _base.Speed + _base.Accessory1.Speed + _base.Accessory2.Speed + Mathf.FloorToInt(((_base.Speed/100f)*level));}
    }

   
}
