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

        foreach (GameObject enemy in enemyList) {
            if (enemy.transform.position.y < -16) {
                enemyList.Remove(enemy);
                Destroy(enemy);
            }
        }
    }

    void SpawnEnemy() {
        Vector3 pos;
        if (Random.Range(0, 2) == 0) {
            pos = new Vector3(27.95f, 8.04f, 0);
        } else {
            pos = new Vector3(-28.38f, 1.94f, 0);
        }
        GameObject newEnemy = Instantiate(enemy, pos, enemy.transform.rotation);
        newEnemy.GetComponent<EnemyScript>().stopped = false;
        enemyList.Add(newEnemy);
    }
}
