using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability 
{
  public AbilityBase _base { get; set;}
  public int cost {get; set;}

  public Ability(AbilityBase aBase){
      _base=aBase;
      cost=_base.Cost;
  }
}
