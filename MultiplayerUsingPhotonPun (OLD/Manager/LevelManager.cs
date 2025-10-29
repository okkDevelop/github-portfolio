using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.Playables;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject StudentUI;
    [SerializeField] private GameObject LecturerUI;
    [SerializeField] private GameObject MapIntro;
    [SerializeField] private SpawnManager SpawnManager;
    [SerializeField] private GameObject StudentSpawnWall;
    [SerializeField] private GameObject LecturerSpawnWall;


    private void Awake()
    {
        AssignTeam();
    }

    private void Start()
    {
        StartCoroutine(DelayedPlayerTeamInfo());
    }

    private void Update()
    {
        LogAllPlayerCustomProperties();
    }
    private void AssignTeamScores()
    {
        Hashtable teamInfo1 = new Hashtable();
        teamInfo1["StudentScore"] = 0;
        PhotonNetwork.CurrentRoom.SetCustomProperties(teamInfo1);
        Hashtable teamInfo2 = new Hashtable();
        teamInfo2["LecturerScore"] = 0;
        PhotonNetwork.CurrentRoom.SetCustomProperties(teamInfo1);

    }
    private void AssignTeam() 
    {
        List<Player> playerList = PhotonNetwork.PlayerList.ToList();

        int studentTeamCount = 0;
        int lecturerTeamCount = 0; 

        while (playerList.Count > 0) 
        {
            int index = Random.Range(0, playerList.Count);
            int teamNumber = 0;
            Player randomPlayer = playerList[index];

            if (studentTeamCount < lecturerTeamCount || studentTeamCount == lecturerTeamCount && Random.value < 0.5f)
            {
                studentTeamCount++;
                teamNumber = 1;
            }
            else
            {
                lecturerTeamCount++;
                teamNumber = 2;
            }

            Hashtable teamInfo = new Hashtable();

            teamInfo["TeamInfo"] = teamNumber;

            randomPlayer.SetCustomProperties(teamInfo);
            playerList.RemoveAt(index);
        }
        AssignTeamScores();
    }

    private void LogAllPlayerCustomProperties()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            string playerName = player.NickName;
            string customData = "";

            // Check if the player has custom properties
            if (player.CustomProperties != null)
            {
                // Iterate over each custom property and log it
                foreach (var entry in player.CustomProperties)
                {
                    customData += $"{entry.Key}: {entry.Value}\n";
                }
            }

            Debug.Log($"Player: {playerName}, Custom Properties:\n{customData}");
        }
    }

    // This method will get the TeamInfo custom property for a specific player
    private void GetPlayerTeamInfo(Player player)
    {
        Debug.Log(player.CustomProperties);
        Debug.Log(player.CustomProperties.ContainsKey("TeamInfo"));

        // Check if the player has custom properties
        if (player.CustomProperties != null && player.CustomProperties.ContainsKey("TeamInfo"))
        {
            Debug.Log("access custom properties");
            object teamInfoObj = player.CustomProperties["TeamInfo"];

            // Check if the property value is of the correct type and is either 1 or 2
            if (teamInfoObj is int teamInfo && (teamInfo == 1 || teamInfo == 2))
            {
                Debug.Log("team system");

                if (teamInfo == 1 && player == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("student");
                    StudentUI.SetActive(true);
                    StartCoroutine(AutoSelectCharacter(teamInfo, StudentUI));
                }
                else if (teamInfo == 2 && player == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("lecturer");
                    LecturerUI.SetActive(true);
                    StartCoroutine(AutoSelectCharacter(teamInfo, LecturerUI));
                }
                else 
                {
                    Debug.Log("this player belong to no team");
                }
            }
            else
            {
                Debug.LogWarning("Player's TeamInfo custom property is not valid.");
            }
        }
        else
        {
            Debug.LogWarning("Player does not have TeamInfo custom property.");
        }
    }

    private IEnumerator DelayedPlayerTeamInfo()
    {
        // Wait for 3 seconds before executing the loop
        yield return new WaitForSeconds(5f);

        MapIntro.SetActive(false);

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GetPlayerTeamInfo(player);
        }
    }

    private IEnumerator AutoSelectCharacter(int TeamInfo, GameObject CharacterUI) 
    {
        yield return new WaitForSeconds(15f);

        int random = Random.Range(0, 4);

        if (TeamInfo == 1 && CharacterUI.active)
        {
            if (random == 0)
            {
                SpawnManager.SpawnImam();
            }
            else if (random == 1)
            {
                SpawnManager.SpawnNsk();
            }
            else if (random == 2)
            {
                SpawnManager.SpawnOzk();
            }
            else if (random == 3) 
            {
                SpawnManager.SpawnAhmad();
            }
            CharacterUI.SetActive(false);
        }
        else if (TeamInfo == 2 && CharacterUI.active)
        {
            if (random == 0)
            {
                SpawnManager.SpawnJacob();
            }
            else if (random == 1)
            {
                SpawnManager.SpawnLai();
            }
            else if (random == 2)
            {
                SpawnManager.SpawnMatthew();
            }
            else if (random == 3)
            {
                SpawnManager.SpawnPuja();
            }
            CharacterUI.SetActive(false);
        }
        else
            Debug.Log("this man has no team");

        StudentSpawnWall.SetActive(false);
        LecturerSpawnWall.SetActive(false);
    }

}
