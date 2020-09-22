using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminationManager : MonoBehaviour
{

    CharacterManager charManager;

    private void Awake()
    {
        charManager = GetComponent<CharacterManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RewardElimination(string name)
    {
        foreach (CharacterInfo c in charManager.characters)
        {
            if (c.info.characterName == name)
            {
                
                c.info.eliminations++;
                Debug.Log(name + " earned an elimination! Total: " + c.info.eliminations.ToString());
            }
        }
    }
}
