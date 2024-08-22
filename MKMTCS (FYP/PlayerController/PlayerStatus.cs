using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private float starveRate;

    private float maxHunger = 100f;
    private float currentHunger;
    [SerializeField] private float hungerRate = 0.9f;

    private float maxMana = 100f;
    private float currentMana;
    [SerializeField] private float restoreRate = 1f;

    public float HealthValue
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public float HungerValue
    {
        get => currentHunger;
        set => currentHunger = Mathf.Clamp(value, 0, maxHunger);
    }

    public float ManaValue 
    {
        get => currentMana;
        set => currentMana = Mathf.Clamp(value, 0, maxMana);
    }

    public float HungerRate 
    {
        get => hungerRate;
        set => hungerRate = value;
    }

    public static PlayerStatus Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentMana = maxMana;
    }

    // Update is called once per frame
    private void Update()
    {
        //for testing purpose
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentHealth -= 10;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            currentMana -= 10;
        }

        HungerDecreasing();
        AutoRestore();
    }

    private void HungerDecreasing() 
    {
        if (currentHunger > 0)
        {
            currentHunger -= hungerRate * Time.deltaTime;
        }
        else if (currentHunger <= 0)
        {
            currentHealth -= starveRate * Time.deltaTime;
        }
    }

    private void AutoRestore()
    {
        if (currentHunger >= 80)
            currentHealth += restoreRate * Time.deltaTime;

        if (currentMana <= maxMana)
            currentMana += restoreRate * Time.deltaTime;
    }
}
