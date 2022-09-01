using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameObject enemy;
    private List<GameObject> enemyList;
    private float timer = 0;

    public int enemySpawnTimer = 3;
    public int maxEnemies = 0;

    void Start()
    {
        enemy = GameObject.Find("Zeca");
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= enemySpawnTimer) {
            if (enemyList.Count < maxEnemies) {
                SpawnEnemy();
            }
            timer = 0f;
        }
    }

    void SpawnEnemy() {
        GameObject newEnemy = Instantiate(enemy, new Vector3(0,0,0), enemy.transform.rotation);
        newEnemy.GetComponent<EnemyScript>().stopped = false;
        enemyList.Add(newEnemy);
    }
}
