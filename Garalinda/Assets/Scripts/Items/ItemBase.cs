using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Item", menuName = "Character/Create new item")]
public class ItemBase : ScriptableObject
{
    [SerializeField] string _name;
    [TextArea]
    [SerializeField] string _description;
    [SerializeField] ItemType itemType;

    //Aplicable solo en las armas, protecciones y accesorios
    [SerializeField] int maxHP;
    [SerializeField] int maxEnergy;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int magicAttack;
    [SerializeField] int magicDefense;
    [SerializeField] int speed;

       public string Name{
        get { return _name; }
    }

    public string Description{
        get { return _description; }
    }

     public ItemType ItemType{
        get { return itemType; }
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
}

public enum ItemType
{
    Arma,
    Protección,
    Accesorio,
    Usable,
    Otro
}