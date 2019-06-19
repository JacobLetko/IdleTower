using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : MonoBehaviour
{
    public List<GameObject> nodes;
    public GameObject Target;
    int index;

    public float speed;
    public float health;
    public int upgrades;
    public float alive;

    GameObject God;

	// Use this for initialization
	void Start ()
    {
        God = GameObject.Find("God");
        respawn();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, (speed * Time.deltaTime));

        alive = alive + Time.deltaTime;

        if (Vector3.Distance(transform.position, Target.transform.position) < 0.000001f)
        {
            if (index == nodes.Count - 1)
                index = 0;
            else
                index++;
            Target = nodes[index];
        }

        if(health <=0)
        {
            this.gameObject.SetActive(false);
            // send data for income
            alive = Mathf.Round(alive * 100f) / 100f;
            God.GetComponent<stats>().Income(upgrades, alive);
        }
	}

    public void respawn()
    {
        this.transform.position = nodes[0].transform.position;
        Target = nodes[0];
        index = 0;
        this.gameObject.SetActive(true);
    }
}
