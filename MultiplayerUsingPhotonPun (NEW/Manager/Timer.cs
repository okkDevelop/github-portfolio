using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    public bool activated;
    public int time;
    float duration = 1f;
    public MechanicManager mm;
    // Start is called before the first frame update
    void Start()
    {
        Hashtable setTime = new Hashtable();
        setTime["Timer"] = 190;
        PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
    }

    // Update is called once per frame
    void Update()
    {


        if (mm.isActiveAndEnabled && activated == false)
        {
            activated = true;
            Hashtable setTime = new Hashtable();
            setTime["Timer"] = 20;
            PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
        }
        time = (int)PhotonNetwork.CurrentRoom.CustomProperties["Timer"];
        if (mm.isEnd == true) time = 0;

        float minutes = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Timer"] / 60);
        float seconds = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Timer"] % 60);
        if (time <= 0)
        {
            timeText.text = "Time's Up!";
        }
        else
        {
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (time > 0)
            {
                duration -= Time.deltaTime;
                if (duration <= 0)
                {
                    timerCountdown();
                    duration = 1f;
                }
            }
            else
            {
                Hashtable setTime = new Hashtable();
                setTime["Timer"] = 0;
                PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
            }



        }
    }
    void timerCountdown()
    {
        Debug.LogError("1");
        Debug.LogError("2");
        int newTime = time - 1;
        Hashtable setTime = new Hashtable();
        setTime["Timer"] = newTime;
        PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
    }

}