using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public HumanType CurrentType;
}
public enum HumanType
{
    Player,
    NPC,
    Zombie
}