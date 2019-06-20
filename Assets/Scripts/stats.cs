using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class stats : MonoBehaviour
{
    [Header("Monster stuff")]
    public int MonsterStrength;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI MovementSpeed;
    public TextMeshProUGUI SpawnRate;
    public TextMeshProUGUI MonsterUpgradeCost;

    [Header("Tower Stuff")]
    public int TowerStrength;
    public TextMeshProUGUI BulletSpeed;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI FireRate;
    public TextMeshProUGUI BulletsPerShoot;
    public TextMeshProUGUI TowerUpgradeCost;

    [Header("Income stuff")]
    public int Cash;
    public float Science;
    public TextMeshProUGUI currCash;
    public TextMeshProUGUI currScience;
    public TextMeshProUGUI TowerCash;
    public TextMeshProUGUI MonsterScience;
    public TextMeshProUGUI IncomePerKill;

    Nodes monsterstuff;
    Tower towerStuff;

    private void Start()
    {
        monsterstuff = GetComponent<Nodes>();
        towerStuff = GetComponent<Tower>();
        Cash = 0;
        Science = 0;
    }

    private void Update()
    {
        UpdateMonster();
        UpdateTower();
    }

    void UpdateMonster()
    {
        // Stats screen
        Health.text = "Health: " + monsterstuff.health;
        MovementSpeed.text = "Speed: " + monsterstuff.speed;
        SpawnRate.text = "SR: " + monsterstuff.ROS;

        // upgrade screen
        MonsterUpgradeCost.text = "upgrade cost: " + CalCost(true);
    }

    void UpdateTower()
    {
        // stats Screen
        BulletSpeed.text = "Bullet Speed: " + towerStuff.bulletSpeed;
        Damage.text = "Damage: " + towerStuff.Damage;
        FireRate.text = "ROF: " + towerStuff.ROF;
        BulletsPerShoot.text = "BPS: " + towerStuff.targets;

        //upgrade Screen
        TowerUpgradeCost.text = "upgrade cost: " + CalCost(false);

        //Money
        currCash.text = "$" + Cash;
        currScience.text = "%" + Science;
        TowerCash.text = "$" + Cash;
        MonsterScience.text = "%" + Science;
        IncomePerKill.text = "$" + MonsterStrength / 2 + " per kill";
    }

    public void Income(int Strength, float Alive)
    {
        Cash += (int)Mathf.Round(Strength / 2);
        Science += Mathf.Round((Alive / 100f) * 100) / 100;
    }

    public int CalCost(bool monster)
    {
        int x = 1;

        if(monster == true)
        {
            for (int i = 0; i < MonsterStrength; i++)
                x += i/2;
        }
        
        if(monster == false)
        {
            for (int i = 0; i < TowerStrength; i++)
                x += i/2;
        }

        return x;
    }
}
