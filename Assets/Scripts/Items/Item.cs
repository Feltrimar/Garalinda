using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
public ItemBase _base { get; set;}

  public Item(ItemBase iBase){
      _base=iBase;
  }
}
