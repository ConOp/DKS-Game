using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    private Basic_Room room;
    private List<GameObject> playersInvolved;
    private List<GameObject> enemiesInvolved;
    private int totalEnemies;
    private int totalActiveEnemies;
    private int remainingEnemies;
    private int numberOfWaves;
    private bool waveFinished;
    public Battle(Basic_Room rm,GameObject player)
    {
        room = rm;
        room.CloseDoors();
        playersInvolved = new List<GameObject>();
        AddPlayer(player);
        enemiesInvolved = new List<GameObject>();
    }
    /// <summary>
    /// Gets the room of the on-going battle.
    /// </summary>
    /// <returns></returns>
    public Basic_Room GetRoom()
    {
        return room;
    }
    /// <summary>
    /// Gets the players involved in the battle.
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetPlayers()
    {
        return playersInvolved;
    }
    /// <summary>
    /// Gets enemies involved in the battle.
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetEnemies()
    {
        return enemiesInvolved;
    }
    /// <summary>
    /// Adds a player to the battle.
    /// </summary>
    /// <param name="player"></param>
    public void AddPlayer(GameObject player)
    {
        playersInvolved.Add(player);
    }
    /// <summary>
    /// Removes a player from the battle.
    /// </summary>
    /// <param name="player"></param>
    public void RemovePlayer(GameObject player)
    {
        playersInvolved.Remove(player);
    }
    /// <summary>
    /// Adds an enemy to the battle.
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemy(GameObject enemy)
    {
        enemiesInvolved.Add(enemy);
    }
    /// <summary>
    /// Removes an enemy from the battle.
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInvolved.Remove(enemy);
        if (enemiesInvolved.Count.Equals(0))
        {
            waveFinished = true;//If there are no enemies left in the battle wave is finished.
        }
    }
    #region EnemySpawn
    public IEnumerator SpawnEnemies()
    {
        totalEnemies = (int)Math.Ceiling(room.Tiles_number_x * room.Tiles_number_z * 0.8f * Difficulty_Manager.GetInstance().GetDifficultyFactor() / 100);
        totalActiveEnemies = (int)Math.Ceiling(0.2f * totalEnemies);
        numberOfWaves = (int)Math.Ceiling((float)totalEnemies / totalActiveEnemies);
        int currentWave = 0;
        int remainingEnemies = totalEnemies;
        Debug.Log("Started Spawning...");
        while (currentWave <= numberOfWaves)
        {
            currentWave++;
            waveFinished = false;
            Debug.Log("Spawning...");
            if (remainingEnemies - totalActiveEnemies >= 0)
            {
                SpawnWave(totalActiveEnemies);
            }
            else
            {
                SpawnWave(remainingEnemies);
            }
            
            Debug.Log("Waiting...");
            yield return new WaitUntil(() => waveFinished);
        }
        Battle_Manager.GetInstance().RemoveBattle(this);
    }
    private void SpawnWave( int numberOfEnemies)
    {
        int spawnedEnemies = 0;
        while (spawnedEnemies < numberOfEnemies)
        {
            int index = UnityEngine.Random.Range(0, room.Instantiated_Tiles.Count - 1);
            Vector3 tileLocation = room.Instantiated_Tiles[index].transform.position;
            Vector3 center = new Vector3((tileLocation.x + (tileLocation.x + Tile.X_length)) / 2f, 2, (tileLocation.z + (tileLocation.z - Tile.Z_length)) / 2f);
            int melee_ranged = UnityEngine.Random.Range(0, 2);
            if (melee_ranged == 0)
            {
                UnityEngine.Object.Instantiate(Enemy_Prefab_Manager.GetInstance().GetMeleeEnemies()[0], center, new Quaternion());

            }
            else
            {
                UnityEngine.Object.Instantiate(Enemy_Prefab_Manager.GetInstance().GetRangedEnemies()[0], center, new Quaternion());
            }
            spawnedEnemies++;
        }
    }
    #endregion
}
