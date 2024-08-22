using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Health : MonoBehaviourPunCallbacks
{
    public float maxHealth = 200;
    [SerializeField] private float currentHealth;
    [SerializeField] private TMP_Text studentScore;
    [SerializeField] private TMP_Text lecturerScore;
    [SerializeField] MechanicManager mm;
    private float timer=1f;
    public int stdScore;
    public int lectScore;
    public float duration;
    SpawnManager spawnManager;
    [SerializeField] Player plyr;

    private void Start()
    {
        Debug.Log("health script start");
        currentHealth = maxHealth;
        spawnManager= GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //mm = GameObject.FindGameObjectWithTag("MechanicManager").GetComponent<MechanicManager>();
        
        if(GameObject.Find("MechanicManager").GetComponent<MechanicManager>()!=null)
        {
            mm = GameObject.Find("MechanicManager").GetComponent<MechanicManager>();
        }
        Debug.Log("mechanic manager found " + mm);
        if (mm.isActiveAndEnabled)
        {
            studentScore.text = "";
            lecturerScore.text = "";
        }
        else if (mm == null)
            Debug.Log("This is mark based game");
        if (PhotonNetwork.IsMasterClient)
        {
            Hashtable addScore1 = new Hashtable();
            addScore1["LecturerScore"] = 0;
            PhotonNetwork.CurrentRoom.SetCustomProperties(addScore1);
            Hashtable addScore2 = new Hashtable();
            addScore2["LecturerScore"] = 0;
            PhotonNetwork.CurrentRoom.SetCustomProperties(addScore2);
        }

    }
    private void Update()
    {
        if (!mm.isActiveAndEnabled || mm==null) 
        {
            if (timer >= 0)
            {
                timer = timer - Time.deltaTime;
                stdScore = (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"];
                lectScore = (int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"];

                studentScore.text = "Student Score = " + stdScore + " %";
                lecturerScore.text = "Lecturer Score = " + lectScore + " %";
                if (stdScore >= 1 || lectScore >= 1)
                {
                    Debug.LogError("it works");
                }

            }
            else
            {
                timer = 1f;
                /*            PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] + 10;
                            PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] + 10;*/
            }
        }
    }
    [PunRPC]
    public void ApplyDamage(float damage)
    {
		Debug.Log("Damage Output!");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    [PunRPC]
    public void ResetHealth( )
    {

        currentHealth = maxHealth;
        if (photonView.Owner.CustomProperties != null && photonView.Owner.CustomProperties.ContainsKey("TeamInfo"))
        {
            Debug.Log("access custom properties");
            object teamInfoObj = photonView.Owner.CustomProperties["TeamInfo"];

            // Check if the property value is of the correct type and is either 1 or 2
            if (teamInfoObj is int teamInfo && (teamInfo == 1 || teamInfo == 2))
            {
                if (teamInfo == 1 )
                {
                    Debug.Log("student");
                    transform.position = spawnManager.GetRandomStudentSpawnPosition();
                    if (photonView.IsMine && Time.time>duration)
                    {
                        int score = (int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] + 20;
                        Hashtable addScore = new Hashtable();
                        addScore["LecturerScore"] = score;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(addScore);
                        duration = Time.time + 3f;
                    }



                }
                else if (teamInfo == 2 )
                {
                    Debug.Log("lecturer");
                    transform.position = spawnManager.GetRandomLecturerSpawnPosition();
                    if (photonView.IsMine && Time.time > duration)
                    {
                        int score = (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] + 20;
                        Hashtable addScore = new Hashtable();
                        addScore["StudentScore"] = score;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(addScore);
                        duration = Time.time + 3f;
                    }

                }

            }

        }
        else
        {
            Debug.LogWarning("Player does not have TeamInfo custom property.");
        }
    }

	[PunRPC]
    private void Die()
    {
        //photonView.RPC("ScoreUpdate", RpcTarget.AllBuffered);
        // Perform any actions when the object dies
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        if (mm.isActiveAndEnabled)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("This is mark based game");
            photonView.RPC("ResetHealth", RpcTarget.AllBuffered);
        }

        //if (mm.gameObject.activeInHierarchy == false)
        //{
        //    photonView.RPC("ResetHealth", RpcTarget.AllBuffered);
        //}
        //else
        //    Destroy(gameObject);
        //ResetHealth();
    }
/*    [PunRPC]
    public void ScoreUpdate()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("TeamInfo"))
        {
            Debug.LogError("running");
            object localplayer = PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"];
            // Check if the property value is of the correct type and is either 1 or 2
            if (localplayer is int teamInfo && (teamInfo == 1 || teamInfo == 2))
            {
                Debug.LogError("running2");
                if (teamInfo == 1 && localplayer == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("student");

                    //PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] + 20;
                    int score = (int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] + 1;
                    Hashtable addScore = new Hashtable();
                    addScore["LecturerScore"] = score;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(addScore);

                }
                else if (teamInfo == 2 && localplayer == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("lecturer");

                    //PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] + 20;
                    int score = (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] + 1;
                    Hashtable addScore = new Hashtable();
                    addScore["StudentScore"] = score;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(addScore);
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
    }*/
}
