using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Character/Create new ability")]
public class AbilityBase : ScriptableObject
{
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [SerializeField] ElementType element;
    [SerializeField] MoveType type;
    [SerializeField] MoveRange range;
    [SerializeField] DamageType damageType;
    [SerializeField] int damage;
    [SerializeField] int cost;

    public string Name{
        get { return _name; }
    }
    public string Description{
        get { return _description; }
    }

      public ElementType Element{
        get { return element; }
    }
      public MoveType Type{
        get { return type; }
    }
      public MoveRange Range{
        get { return range; }
    }
      public DamageType DamageType{
        get { return damageType; }
    }
      public int Damage{
        get { return damage; }
    }
      public int Cost{
        get { return cost; }
    }


}




public enum ElementType
{
    Neutro,
    Fuego,
    Agua,
    Viento,
    Tierra,
    Oscuridad,
    Luz
}

public enum MoveRange
{
    Punto,
    Linea,
    Area
}

public enum MoveType
{
    Ofensivo,
    Apoyo
}

public enum DamageType
{
 Físico,
 Mágico
}