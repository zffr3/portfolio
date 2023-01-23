using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagble
{
    void TakeDamage(float damage, string plrName);
    void AddHealth(float healthCount);
}
