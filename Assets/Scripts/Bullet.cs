using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //Variables
    private float speed;
    private float maxDistance;
    private float damage;

    private float distance = 0;

    //Methods

    public void Initialize(float speed, float maxDistance, float damage)
    {
        this.speed = speed;
        this.maxDistance = maxDistance;
        this.damage = damage;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        distance += 1 * Time.deltaTime;

        if (distance >= maxDistance)
            Destroy(this.gameObject);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameObject triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            GameObject triggeringPlayer = other.gameObject;
            triggeringPlayer.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
