using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public Transform ProjectileTarget;
    public float ProjectileSpeed;

    private Vector3 InitialTargetPosition;
    private Vector3 InitialDirection;

    // Start is called before the first frame update
    void Start()
    {
        InitialTargetPosition = ProjectileTarget.position;
        InitialDirection = (InitialTargetPosition - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(InitialDirection * ProjectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy hit!");

            EnemyController hitEnemyController = collision.GetComponent<EnemyController>();

            if (hitEnemyController != null)
            {
                hitEnemyController.Health -= 10;

                Destroy(gameObject);
            }
        }
    }
}
