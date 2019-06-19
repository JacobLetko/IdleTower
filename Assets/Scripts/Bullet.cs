using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float damage;

    Rigidbody rb;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<MoveMonster>().health -= damage;
            this.gameObject.SetActive(false);
            rb.isKinematic = true;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (target == null || target.gameObject.activeSelf == false)
            this.gameObject.SetActive(false);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (speed * Time.deltaTime));
        if (Vector3.Distance(transform.position, target.transform.position) < 1)
        {
            rb.isKinematic = false;
        }
    }
}
