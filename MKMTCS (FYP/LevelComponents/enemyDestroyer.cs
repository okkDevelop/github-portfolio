using EmeraldAI;
using UnityEngine;

public class enemyDestroyer : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EmeraldAISystem>() != null && nameof(other.gameObject) != "Golem_Ice" && nameof(other.gameObject) != "DEMON_LORD" && nameof(other.gameObject) != "Necromancer") 
        {
            spawner.EnemiesRemove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
