using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterBase _base;
    public int level;
    public int experience;

    public int HP {get; set;}
    public int Energy {get; set;}
    
    public List<Ability> MainAbilities {get; set;}
    public List<Ability> SupportAbilities {get; set;}
    public GameObject node;
    public BattleState state;
    public bool enemy;
    public bool defeated;
    public ItemBase weapon;
    public ItemBase armor;
    public ItemBase accessory1;
    public ItemBase accessory2;
    public int attackDamage;
    public int healing;

    public void Setup(CharacterBase chbase, int chLevel){

        _base=chbase;
        level=chLevel;
        HP = MaxHP;
        Energy = MaxEnergy;
        weapon=chbase.Weapon;
        armor=chbase.Armor;
        accessory1=chbase.Accessory1;
        accessory2=chbase.Accessory2;

        
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

   public bool ReceiveAttack(Ability ab, Character ch){
       if(ab._base.DamageType==DamageType.Físico)
            attackDamage = ch.Attack+ab._base.Damage-Defense;
        else
            attackDamage = ch.MagicAttack+ab._base.Damage-MagicDefense;
       if (attackDamage<0)
        attackDamage= -attackDamage;
       HP-=attackDamage;
       if(HP<0)
       {
           HP = 0;
           return true;
       }
       else{
           return false;
       }

    }

     public void ReceiveHeal(Ability ab, Character ch){
       if(ab._base.DamageType==DamageType.Físico)
            healing = ch.Attack+ab._base.Damage;
        else
            healing = ch.MagicAttack+ab._base.Damage;
       HP+=healing;
       if(HP>MaxHP)
       {
           HP = MaxHP;
       }

    }

    public void ItemHeal(ItemBase it){
    if(it.HealItem){
       healing=it.Healing;
       HP+=healing;
       if(HP>MaxHP)
       {
           HP = MaxHP;
       }
    }

    if(it.EnergyItem){
       healing=it.Energy; 
       Energy+=healing;
       if(Energy>MaxEnergy)
       {
           Energy = MaxEnergy;
       }
    }

    }

    public Ability GetRandomAbility(){
        System.Random rnd = new System.Random();
        var i=rnd.Next(0,MainAbilities.Count);
        return MainAbilities[i];
    }

    public void getNewAttacks(){
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
    
}
