using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject Camera;

    public Transform main;
    public Transform towerUpgrades;
    public Transform MonsterUpgrades;
    public Transform Stats;
    public float speed;

    Transform Target;

    private void Start()
    {
        Target = main;
        Camera.transform.position = main.position;
    }

    private void Update()
    {
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, Target.transform.position, (speed * Time.deltaTime));
    }

    public void MoveToTower ()
    {
        Target = towerUpgrades;
    }

    public void MoveToMonster()
    {
        Target = MonsterUpgrades;
    }

    public void MoveToStats()
    {
        Target = Stats;
    }
    public void MoveToMain()
    {
        Target = main;
    }
}
