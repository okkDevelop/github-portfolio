using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class Projectile : MonoBehaviour
{
    [Header("Projectiels Settings")]
    [SerializeField] private int damageAmount;
    [SerializeField] private float existTime;
    [SerializeField] private float flyingSpeed;
    [SerializeField] private LayerMask collideToDestroy;
    private float currentExistTime;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        currentExistTime -= Time.deltaTime;

        if (currentExistTime <= 0)
            ObjectPooling.Instance.StoreProjectile(gameObject);
    }

    private void OnEnable()
    {
        currentExistTime = existTime;
        rigidBody.velocity = transform.forward * flyingSpeed;
    }

    private void OnDisable()
    {
        rigidBody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & collideToDestroy) != 0)
        {
            SoundsManager.Instance.PlaySound("FireExplore");
            ObjectPooling.Instance.StoreProjectile(gameObject);

            EmeraldAISystem detectedEnemy = other.gameObject.GetComponent<EmeraldAISystem>();

            if (detectedEnemy != null) 
                detectedEnemy.Damage(damageAmount, EmeraldAI.EmeraldAISystem.TargetType.Player, gameObject.transform, 0);
        }
    }
}
