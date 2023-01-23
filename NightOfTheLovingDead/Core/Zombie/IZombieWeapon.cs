using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieWeapon
{
    void Attack(Human target);
    void ConfigureWeapon(float attackDistance, float damage);
}
