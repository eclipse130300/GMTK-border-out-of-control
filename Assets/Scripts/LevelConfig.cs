using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level Data", menuName = "New Level data")]
public class LevelConfig : ScriptableObject
{
    public int minWaveSpawnAmount;
    public int maxWaveSpawnAmount;
    
    public int minSoloSpawnDelay;
    public int maxSoloSpawnDelay;
    
    public int waveDelay;

    public float enemyMoveSpeed;
}
