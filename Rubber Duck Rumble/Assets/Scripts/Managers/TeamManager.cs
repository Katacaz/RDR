using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public CharacterManager charManager;
    [Header("Team 0")]
    public string team0Name = "Gray";
    public Color team0Color = Color.gray;
    public List<CharacterInfo> team0;
    int total0 = 0;

    [Header("Team 1")]
    public string team1Name = "Blue";
    public Color team1Color = Color.blue; 
    public List<CharacterInfo> team1;
    int total1 = 0;

    [Header("Team 2")]
    public string team2Name = "Red";
    public Color team2Color = Color.red;
    public List<CharacterInfo> team2;
    int total2 = 0;

    [Header("Team 3")]
    public string team3Name = "Yellow";
    public Color team3Color = Color.yellow;
    public List<CharacterInfo> team3;
    int total3 = 0;

    [Header("Team 4")]
    public string team4Name = "Green";
    public Color team4Color = Color.green;
    public List<CharacterInfo> team4;
    int total4 = 0;

    [Header("Team 5")]
    public string team5Name = "Cyan";
    public Color team5Color = Color.cyan;
    public List<CharacterInfo> team5;
    int total5 = 0;

    [Header("Team 6")]
    public string team6Name = "Magenta";
    public Color team6Color = Color.magenta;
    public List<CharacterInfo> team6;
    int total6 = 0;

    [Header("Team 7")]
    public string team7Name = "White";
    public Color team7Color = Color.white;
    public List<CharacterInfo> team7;
    int total7 = 0;

    [Header("Team 8")]
    public string team8Name = "Black" ;
    public Color team8Color = Color.black;
    public List<CharacterInfo> team8;
    int total8 = 0;

    // Start is called before the first frame update
    void Start()
    {
        charManager = FindObjectOfType<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetTeams()
    {
        total0 = 0;
        total1 = 0;
        total2 = 0;
        total3 = 0;
        total4 = 0;
        total5 = 0;
        total6 = 0;
        total7 = 0;
        total8 = 0;

        team0.Clear();
        team1.Clear();
        team2.Clear();
        team3.Clear();
        team4.Clear();
        team5.Clear();
        team6.Clear();
        team7.Clear();
        team8.Clear();
    }
    public void SeperateTeams()
    {
        ResetTeams();
        //first get totals for each team
        foreach (CharacterInfo c in charManager.characters)
        {
            int teamID = c.info.teamID;
            if (teamID == 0)
            {
                //if gray Team
                total0++;
                team0.Add(c);
            } 
            else if (teamID == 1)
            {
                //if Blue Team
                total1++;
                team1.Add(c);
            }
            else if (teamID == 2)
            {
                //if Red team
                total2++;
                team2.Add(c);
            }
            else if (teamID == 3)
            {
                //if Yellow team
                total3++;
                team3.Add(c);
            }
            else if (teamID == 4)
            {
                // if Green team
                total4++;
                team4.Add(c);
            }
            else if (teamID == 5)
            {
                //if Cyan team
                total5++;
                team5.Add(c);
            }
            else if (teamID == 6)
            {
                //if Magenta team
                total6++;
                team6.Add(c);
            }
            else if (teamID == 7)
            {
                //if white team
                total7++;
                team7.Add(c);
            }
            else if (teamID == 8)
            {
                //if Black Team
                total8++;
                team8.Add(c);
            }
            
        }
    }
    public string TeamIDtoName(int teamID)
    {
        string teamName = "";
        if (teamID == 0)
        {
            //if gray Team
            teamName = team0Name;
        }
        else if (teamID == 1)
        {
            //if Blue Team
            teamName = team1Name;
        }
        else if (teamID == 2)
        {
            //if Red team
            teamName = team2Name;
        }
        else if (teamID == 3)
        {
            //if Yellow team
            teamName = team3Name;
        }
        else if (teamID == 4)
        {
            // if Green team
            teamName = team4Name;
        }
        else if (teamID == 5)
        {
            //if Cyan team
            teamName = team5Name;
        }
        else if (teamID == 6)
        {
            //if Magenta team
            teamName = team6Name;
        }
        else if (teamID == 7)
        {
            //if white team
            teamName = team7Name;
        }
        else if (teamID == 8)
        {
            //if Black team
            teamName = team8Name;
        }

        return teamName;
    }

    public Color TeamIDtoColor(int teamID)
    {
        Color teamColor = Color.gray;
        if (teamID == 0)
        {
            //if grey Team
            teamColor = team0Color;
        }
        else if (teamID == 1)
        {
            //if Blue Team
            teamColor = team1Color;
        }
        else if (teamID == 2)
        {
            //if Red team
            teamColor = team2Color;
        }
        else if (teamID == 3)
        {
            //if Yellow team
            teamColor = team3Color;
        }
        else if (teamID == 4)
        {
            // if Green team
            teamColor = team4Color;
        }
        else if (teamID == 5)
        {
            //if Cyan team
            teamColor = team5Color;
        }
        else if (teamID == 6)
        {
            //if Magenta team
            teamColor = team6Color;
        }
        else if (teamID == 7)
        {
            //if white team
            teamColor = team7Color;
        }
        else if (teamID == 8)
        {
            //if Black team
            teamColor = team8Color;
        }
        return teamColor;
    }
}
