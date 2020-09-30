using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 5;

    public int currentHealth = 5;

    public GameObject damagedEffectPrefab;
    public GameObject deathEffectPrefab;

    private void OnEnable()
    {
        currentHealth = startingHealth;
    }
    
    public void TakeDamage(int amount, string characterName)
    {
        currentHealth -= amount;
        if (damagedEffectPrefab != null)
        {
            GameObject effect = Instantiate(damagedEffectPrefab);
            effect.transform.position = this.transform.position;
            Destroy(effect, 3.0f);

        }
        if (currentHealth <= 0)
        {
            Death(characterName);
        }
    }
    public void CallKnockBack(Vector3 direction)
    {
        if (currentHealth > 1)
        {
            if (GetComponent<ObjectKnockBack>() != null)
            {
                GetComponent<ObjectKnockBack>().KnockBack(direction);
            }
            if (GetComponent<PlayerController>())
            {
                GetComponent<PlayerController>().KnockBack(direction);
            }
        }
    }
    private void Death(string name)
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab);
            effect.transform.position = this.transform.position;
            Destroy(effect, 3.0f);
        }
        if (GetComponent<CharacterInfo>() != null)
        {
            EliminationManager elimManager = FindObjectOfType<EliminationManager>();
            if (elimManager != null)
            {
                elimManager.RewardElimination(name);
                elimManager.IncreastDeathCounter(this.GetComponent<CharacterInfo>().info.characterName);
            }
            RespawnManager spawnManager = FindObjectOfType<RespawnManager>();
            if (spawnManager.canRespawn)
            {
                if (GetComponent<PlayerController>())
                {
                    GetComponent<PlayerController>().StopKnockBack();
                    SendMessage("OnDeath");
                }
                spawnManager.RespawnCharacter(this.gameObject);
            }
        }
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
