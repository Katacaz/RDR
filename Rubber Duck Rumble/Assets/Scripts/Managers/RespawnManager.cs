using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RespawnManager : MonoBehaviour
{

    public bool canRespawn;
    [Header("Stock Settings")]
    public bool isStock;
    public int lives;

    public Transform[] respawnLocations;

    public float respawnTime = 2.0f;

    CharacterManager charManager;

    public float respawnSafeDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        charManager = FindObjectOfType<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStockLives()
    {
        
        foreach (CharacterInfo c in charManager.characters)
        {
            c.info.lives = lives;
        }
    }
    public void RespawnCharacter(GameObject character)
    {
        if (character.GetComponent<PlayerController>())
        {
            character.GetComponent<PlayerController>().StopKnockBack();
        }
        bool willRespawn = true;
        if (isStock)
        {
            //if playing a Stock game mode
            if (character.GetComponent<CharacterInfo>().info.lives > 0)
            {
                //if player has lives remaining keep allowing respawn and minus life.
                character.GetComponent<CharacterInfo>().info.lives--;

            } else
            {
                //if no more lives, prevent respawn
                willRespawn = false;
            }
        }
        if (willRespawn)
        {
            bool validSpawn = false;
            if (respawnLocations.Length > 0)
            {
                 
                int location = Random.Range(0, respawnLocations.Length);

                validSpawn = respawnLocations[location].GetComponent<RespawnPointChecker>().safeSpawn;

                if (!validSpawn)
                {
                    //Debug.Log(respawnLocations[location].name + " is not a safe respawn point");
                    RespawnCharacter(character);

                } else
                {
                    if (respawnLocations[location].GetComponent<RespawnPointChecker>().usedSpawn == false)
                    {
                        //if respawn location has not spawned anything recently
                        if (character.GetComponent<NavMeshAgent>() != null)
                        {
                            // if the respawning character is an AI
                            character.GetComponent<NavMeshAgent>().Warp(respawnLocations[location].position);
                        }
                        else
                        {
                            
                            character.transform.position = respawnLocations[location].position;
                        }
                        respawnLocations[location].GetComponent<RespawnPointChecker>().usedSpawn = true;
                        StartCoroutine(respawnWait(character));

                    } else
                    {
                        //Debug.Log(respawnLocations[location].name + " was recently used");
                        RespawnCharacter(character);
                    }

                }



            }
        } else
        {
            
            character.SetActive(false);
            //Debug.Log(character.GetComponent<CharacterInfo>().info.characterName + " has been eliminated.");
        }
    }

    IEnumerator respawnWait(GameObject character)
    {
        yield return new WaitForSeconds(respawnTime);
        character.SetActive(true);
    }
}
