using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour {

    public List<GameObject> nodes;

    public Color LineColor;

    public GameObject Monster;
    List<GameObject> monsters;
    List<GameObject> sendToTower;
    Tower tower;

    public float speed;
    public float health;
    public float ROS;
    public float ROSTimer;

    stats stats;

    private void Start()
    {
        tower = GetComponent<Tower>();
        monsters = new List<GameObject>();
        ROSTimer = ROS;
        stats = GetComponent<stats>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = LineColor;

        Vector3 currNode = Vector3.zero;
        Vector3 prevNode = Vector3.zero;

        for (int i = 0; i < nodes.Count; i++)
        {
            if (i == 0 && nodes.Count > 1)
                prevNode = nodes[nodes.Count - 1].transform.position;
            if (i > 0)
                prevNode = nodes[i - 1].transform.position;

             currNode = nodes[i].transform.position;

            Gizmos.DrawLine(currNode, prevNode);
        }
    }

    public void spawn()
    {
        if (monsters.Count < 1)
        {
            GameObject mon = Instantiate(Monster, nodes[0].transform);
            mon.GetComponent<MoveMonster>().nodes = nodes;
            mon.GetComponent<MoveMonster>().health = health;
            mon.GetComponent<MoveMonster>().speed = speed;
            mon.GetComponent<MoveMonster>().upgrades = stats.MonsterStrength;
            monsters.Add(mon);
        }

        else
        {
            int count = 0;
            for(int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].activeSelf == false)
                {
                    monsters[i].GetComponent<MoveMonster>().health = health;
                    monsters[i].GetComponent<MoveMonster>().speed = speed;
                    monsters[i].GetComponent<MoveMonster>().upgrades = stats.MonsterStrength;
                    monsters[i].GetComponent<MoveMonster>().respawn();
                    return;
                }
                else if (monsters[i].activeSelf == true)
                {
                    count++;
                }
            }
            if(count == monsters.Count)
            {
                GameObject mon = Instantiate(Monster, nodes[0].transform);
                mon.GetComponent<MoveMonster>().nodes = nodes;
                mon.GetComponent<MoveMonster>().health = health;
                mon.GetComponent<MoveMonster>().speed = speed;
                mon.GetComponent<MoveMonster>().upgrades = stats.MonsterStrength;
                monsters.Add(mon);
            }
        }
    }

    private void Update()
    {
        ROSTimer = ROSTimer - Time.deltaTime;
        if (ROSTimer <= 0)
        {
            spawn();
            ROSTimer = ROS;
        }

        sendToTower = new List<GameObject>();
        for(int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].activeSelf == true)
                sendToTower.Add(monsters[i]);
        }
        tower.monsters = sendToTower;
    }

    public void UpgradeHealth()
    {
        if (stats.CalCost(true) < stats.Science)
        {
            health++;
            stats.Science = Mathf.Round((stats.Science - stats.CalCost(true)) * 100) / 100;
            GetComponent<stats>().MonsterStrength++;
        }
    }

    public void UpgradeSpeed()
    {
        if (stats.CalCost(true) < stats.Science)
        {
            speed += .1f;
            stats.Science = Mathf.Round((stats.Science - stats.CalCost(true)) * 100) / 100;
            GetComponent<stats>().MonsterStrength++;
        }
    }

    public void UpgradeROS()
    {
        if (stats.CalCost(true) < stats.Science)
        {
            ROS -= .01f;
            ROS = Mathf.Round(ROS * 100f) / 100f;
            stats.Science = Mathf.Round((stats.Science - stats.CalCost(true)) * 100) / 100;
            GetComponent<stats>().MonsterStrength++;
        }
    }

    public void ResetStats()
    {
        health = 1;
        speed = 1;
        ROS = 10;
        ROSTimer = ROS;

        tower.Damage = 1;
        tower.bulletSpeed = 1.5f;
        tower.targets = 1;
        tower.ROF = 10;
        tower.ROFTimer = tower.ROF;

        stats.Cash = 0;
        stats.Science = 0;

        for(int i = 0; i < monsters.Count; i++)
        {
            monsters[i].SetActive(false);
        }
    }
}
