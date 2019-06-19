using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject tower;
    public float Damage;
    public float bulletSpeed;
    public int targets;
    public float ROF;
    public float ROFTimer;

    public GameObject Bullet;
    List<GameObject> bullets;

    stats stats;
    
    public List<GameObject> monsters;
    private void Start()
    {
        monsters = new List<GameObject>();
        bullets = new List<GameObject>();
        ROFTimer = ROF;
        stats = GetComponent<stats>();
    }

    private void Update()
    {
        ROFTimer = ROFTimer - Time.deltaTime;
        if (ROFTimer <= 0)
        {
            if (monsters.Count > 0)
                for (int i = 0; i < targets; i++)
                {
                Shoot(monsters[Random.Range(0, (monsters.Count))]);
                }
            ROFTimer = ROF;
        }
    }

    void Shoot(GameObject Tar)
    {
        if (bullets.Count < 1)
        {
            GameObject bull = Instantiate(Bullet, tower.transform);
            bull.GetComponent<Bullet>().target = Tar.transform;
            bull.GetComponent<Bullet>().damage = Damage;
            bull.GetComponent<Bullet>().speed = bulletSpeed;
            bullets.Add(bull);
        }
        else
        {
            int count = 0;
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].activeSelf == false)
                {
                    bullets[i].GetComponent<Bullet>().target = Tar.transform;
                    bullets[i].GetComponent<Bullet>().damage = Damage;
                    bullets[i].GetComponent<Bullet>().speed = bulletSpeed;
                    bullets[i].transform.position = tower.transform.position;
                    bullets[i].SetActive(true);
                    return;
                }
                else if (bullets[i].activeSelf == true)
                    count++;
            }
            if(count == bullets.Count)
            {
                GameObject bull = Instantiate(Bullet, tower.transform);
                bull.GetComponent<Bullet>().target = Tar.transform;
                bull.GetComponent<Bullet>().damage = Damage;
                bull.GetComponent<Bullet>().speed = bulletSpeed;
                bullets.Add(bull);
            }
        }
    }

    public void UpgradeROF()
    {
        if (stats.CalCost(false) <= stats.Cash)
        {
            ROF -= .01f;
            ROF = Mathf.Round(ROF * 100f) / 100f;
            stats.Cash -= stats.CalCost(false);
            GetComponent<stats>().TowerStrength++;
        }
    }

    public void UpgradeDamage()
    {
        if (stats.CalCost(false) <= stats.Cash)
        {
            Damage++;
            stats.Cash -= stats.CalCost(false);
            GetComponent<stats>().TowerStrength++;
        }
    }

    public void upgradeSpeed()
    {
        if (stats.CalCost(false) <= stats.Cash)
        {
            bulletSpeed += .1f;
            stats.Cash -= stats.CalCost(false);
            GetComponent<stats>().TowerStrength++;
        }
    }

    public void UpgradeTargets()
    {
        if (stats.CalCost(false) <= stats.Cash)
        {
            targets++;
            stats.Cash -= stats.CalCost(false);
            GetComponent<stats>().TowerStrength++;
        }
    }
}
