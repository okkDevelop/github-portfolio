using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    [Header("RayCast Setting")]
    [SerializeField] private float rayCastRange = 2f;
    [SerializeField] private LayerMask InteractableLayer;
    [SerializeField] private GameObject objectDetected;

    [Header("Enemy Related Setting")]
    [SerializeField] private LayerMask enemiesLayer;

    [Header("Tool Setting")]
    [SerializeField] private ToolSelection toolSelection;
    private int damageIndex;

    [Header("Projectile Setting")]
    [SerializeField] private float fireBallManaSpent;
    [SerializeField] private Transform castingPoint;

    private float delay = 0.5f;

    private void Update()
    {
        StartCoroutine(DetectObject());

        if (Input.GetMouseButtonDown(1) && toolSelection.IsMagic && PlayerStatus.Instance.ManaValue > fireBallManaSpent) 
        {
            CastFireball();
            PlayerStatus.Instance.ManaValue -= fireBallManaSpent;
        }
    }

    private IEnumerator DetectObject() 
    {
        Ray detector = new Ray(transform.position, transform.TransformDirection(Vector3.forward * rayCastRange)); //declare the ray cast
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * rayCastRange)); //show the ray cast in scene

        bool isAxe = toolSelection.IsAxe;
        bool isPickaxe = toolSelection.IsPickaxe;
        bool isSword = toolSelection.IsSword;

        if (Physics.Raycast(detector, out RaycastHit interactableDetector, rayCastRange, InteractableLayer))
        {
            if (objectDetected == null)
            {
                objectDetected = interactableDetector.collider.gameObject;
                objectDetected.AddComponent<QOutline>();
            }
            else
            {
                if (objectDetected != interactableDetector.collider.gameObject)
                {
                    var objectDetectedOutline = objectDetected.GetComponent<QOutline>();
                    Destroy(objectDetectedOutline);
                    objectDetected = interactableDetector.collider.gameObject;
                    objectDetected.AddComponent<QOutline>();
                }
            }

            StartCoroutine(InteractableObject(isAxe, isPickaxe));

            CollectableObject();
        }
        else if (Physics.Raycast(detector, out RaycastHit enemiesDetector, rayCastRange, enemiesLayer))
        {
            EmeraldAISystem detectedEnemy = enemiesDetector.collider.gameObject.GetComponent<EmeraldAISystem>();

            if (objectDetected == null)
            {
                objectDetected = enemiesDetector.collider.gameObject;
                objectDetected.AddComponent<QOutline>();
            }
            else
            {
                if (objectDetected != enemiesDetector.collider.gameObject)
                {
                    var objectDetectedOutline = objectDetected.GetComponent<QOutline>();
                    Destroy(objectDetectedOutline);
                    objectDetected = enemiesDetector.collider.gameObject;
                    objectDetected.AddComponent<QOutline>();
                }
            }

            if (detectedEnemy != null && Input.GetMouseButtonDown(0))
            {
                SoundsManager.Instance.PlaySound("SwordHit");

                damageIndex = isSword ? 5 : 1;

                yield return new WaitForSeconds(delay);

                detectedEnemy.Damage(damageIndex, EmeraldAI.EmeraldAISystem.TargetType.Player, gameObject.transform, 0);
            }
        }
        else
        {
            if (objectDetected != null)
            {
                var objectDetectedOutline = objectDetected.GetComponent<QOutline>();
                Destroy(objectDetectedOutline);
                objectDetected = null;
            }
        }
    }

    private IEnumerator InteractableObject(bool isAxe, bool isPickaxe) 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interactable interacting = objectDetected.GetComponent<Interactable>();

            if (interacting != null)
            {
                if (interacting.CompareTag("Wooden"))
                {
                    SoundsManager.Instance.PlaySound("Chop");

                    damageIndex = isAxe ? 5 : 1;

                    yield return new WaitForSeconds(delay);

                    interacting.Interacting(damageIndex);
                }
                else if (interacting.CompareTag("stone"))
                {
                    SoundsManager.Instance.PlaySound("Mining");

                    damageIndex = isPickaxe ? 5 : 1;

                    yield return new WaitForSeconds(delay);

                    interacting.Interacting(damageIndex);
                }
            }
        }
    }

    private void CollectableObject() 
    {
        ItemData itemData = objectDetected.GetComponent<ItemData>();

        if (itemData != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SoundsManager.Instance.PlaySound("PickUp");
                Inventory.Instance.AddItem(itemData.items, 1);
                Destroy(itemData.gameObject);
            }
        }
    }

    private void CastFireball()
    {
        Ray detector = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(detector, out RaycastHit hit, rayCastRange))
        {
            Vector3 fireballDirection = hit.point - castingPoint.position;
            Quaternion fireballRotation = Quaternion.LookRotation(fireballDirection);
            GameObject fireball = ObjectPooling.Instance.GetProjectile(castingPoint.position, fireballRotation);
        }
        else
        {
            // If no hit, cast fireball straight forward
            GameObject fireball = ObjectPooling.Instance.GetProjectile(castingPoint.position, Quaternion.LookRotation(transform.forward));
        }
    }

}