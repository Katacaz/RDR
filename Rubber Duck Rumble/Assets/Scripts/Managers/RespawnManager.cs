using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{

    public bool canRespawn;
    [Header("Stock Settings")]
    public bool isStock;
    public int lives;

    public Transform[] respawnLocations;

    public float respawnTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStockLives()
    {
        CharacterManager charManager = FindObjectOfType<CharacterManager>();
        foreach (CharacterInfo c in charManager.characters)
        {
            c.info.lives = lives;
        }
    }
    public void RespawnCharacter(GameObject character)
    {
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
            if (respawnLocations.Length > 0)
            {
                int location = Random.Range(0, respawnLocations.Length);

                character.transform.position = respawnLocations[location].position;

                StartCoroutine(respawnWait(character));
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
