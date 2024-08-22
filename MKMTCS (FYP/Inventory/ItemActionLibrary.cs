using UnityEngine;

public class ItemActionLibrary : MonoBehaviour
{
    public static ItemActionLibrary Instance;
    [SerializeField] private Transform placeTransform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Consume(Items_SO Items)
    {
        if (Items.ComsumableType == ConsumableTypes.ForHealth)
        {
            PlayerStatus.Instance.HealthValue += Items.number;
            GameEventsManager.Instance.questEvents.HealthRegain();
            SoundsManager.Instance.PlaySound("Heal");
        }
        else if (Items.ComsumableType == ConsumableTypes.ForHunger)
        {
            PlayerStatus.Instance.HungerValue += Items.number;
            GameEventsManager.Instance.questEvents.EatingFood();
            SoundsManager.Instance.PlaySound("Hiccup");
        }
        else if (Items.ComsumableType == ConsumableTypes.ForMana)
        {
            PlayerStatus.Instance.ManaValue += Items.number;
            GameEventsManager.Instance.questEvents.ManaRegain();
            SoundsManager.Instance.PlaySound("Regain");
        }

        Inventory.Instance.RemoveItem(Items, 1);
    }

    public void Place(GameObject objectToPlace)
    {
        Instantiate(objectToPlace, placeTransform.position, placeTransform.rotation);
        SoundsManager.Instance.PlaySound("Put");
    }
}