using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public BattleManager Battle;
    public GameManager Manager;
    public List<GameObject> Enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        
        var enemyPicker = Random.Range(0, Enemies.Count);
        var newEnemy = Instantiate(Enemies[enemyPicker], transform);
        Battle.StartBattle(newEnemy.GetComponent<Enemy>());
        GameManager.Paused = true;
    }
    public void SpawnBoss(Enemy e, BossFight b)
    {
        var newBoss = Instantiate(e, b.transform);
        Battle.StartBattle(newBoss.GetComponent<Enemy>());
        GameManager.Paused = true;
        


    }

}
