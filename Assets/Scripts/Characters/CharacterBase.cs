using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Character", menuName = "Character/Create new playable character")]
public class CharacterBase : ScriptableObject
{
    [SerializeField] string _name;
    [TextArea]
    [SerializeField] string _description;
    [SerializeField] Sprite image;
    //Stats
    [SerializeField] int maxHP;
    [SerializeField] int maxEnergy;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int magicAttack;
    [SerializeField] int magicDefense;
    [SerializeField] int speed;
    [SerializeField] ItemBase weapon;
    [SerializeField] ItemBase armor;
    [SerializeField] ItemBase accessory1;
    [SerializeField] ItemBase accessory2;
    [SerializeField] List<AbilityLearn> learnableAbilitiesMain;
    [SerializeField] List<AbilityLearn> learnableAbilitiesSupport;
    [SerializeField] GameObject actualNode;
    public string Name{
        get { return _name; }
    }

    public string Description{
        get { return _description; }
    }

     public Sprite Image{
        get { return image; }
    }

     public int MaxHP{
        get { return maxHP; }
    }

    public int MaxEnergy{
        get { return maxEnergy; }
    }

     public int Attack{
        get { return attack; }
    }

    public int Defense{
        get { return defense; }
    }

    public int MagicAttack{
        get { return magicAttack; }
    }

    public int MagicDefense{
        get { return magicDefense; }
    }

    public int Speed{
        get { return speed; }
    }

    public ItemBase Weapon{
        get { return weapon; }
    }

    public ItemBase Armor{
        get { return armor; }
    }

    public ItemBase Accessory1{
        get { return accessory1; }
    }

    public ItemBase Accessory2{
        get { return accessory2; }
    }


       public List<AbilityLearn> LearnableAbilitiesMain{
        get { return learnableAbilitiesMain; }
    }

        public List<AbilityLearn> LearnableAbilitiesSupport{
        get { return learnableAbilitiesSupport; }
    }

    public GameObject ActualNode{
        get { return actualNode; }
    }


    
}
[System.Serializable]
public class AbilityLearn
{
    [SerializeField] AbilityBase abilityBase;
    [SerializeField] int level;

     public AbilityBase Base{
        get { return abilityBase; }
    }
      public int Level{
        get { return level; }
    }

}

public enum BattleState
{
    Principal,
    Apoyo,
    Reserva
}
