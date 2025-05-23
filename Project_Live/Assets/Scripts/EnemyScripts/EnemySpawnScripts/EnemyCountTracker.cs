using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountTracker : MonoBehaviour
{
    EnemyType enemyType;
    int prev_Count;

    public EnemyCountTracker(EnemyType enemyType)
    {
        this.enemyType = enemyType;
        prev_Count = -1;
    }

    public bool HasChanged(out int currentCount) //敵の数が変わったどうか判定する
    {
        currentCount = EnemyRegistry.GetCount(enemyType);

        if (currentCount != prev_Count)
        {
            prev_Count = currentCount;
            return true;
        }

        return false;
    }
}
