using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject WinPlayAgainButton;
    [SerializeField] private GameObject DrawPlayAgainButton;
    [SerializeField] private GameObject LostPlayAgainButton;
    [SerializeField] private Button WinReplayButton;
    [SerializeField] private Button DrawReplayButton;
    [SerializeField] private Button LostReplayButton;
    [SerializeField] private Timer timer;
    bool startCheck;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        if(startCheck)
        {
            if (timer.time > 0)
            {
                CheckScore();
            }
            else
            {
                TimerEnd();
            }
        }

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(20f);
        startCheck = true;
    }
    void CheckScore()
    {
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] >= 100 && (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] < 100)
        {
            PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] = 100;
            if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
            {
                WinPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    WinReplayButton.interactable = false;
                }
                else
                    WinReplayButton.interactable = true;
            }
            else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
            {
                LostPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    LostReplayButton.interactable = false;
                }
                else
                    LostReplayButton.interactable = true;
            }
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] >= 100 && (int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] < 100)
        {
            PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] = 100;
            if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
            {
                LostPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    LostReplayButton.interactable = false;
                }
                else
                    LostReplayButton.interactable = true;
            }
            else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
            {
                WinPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    WinReplayButton.interactable = false;
                }
                else
                    WinReplayButton.interactable = true;
            }
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] >= 100 && (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] >= 100)
        {
            PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] = 100;
            PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] = 100;
            if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
            {
                WinPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    WinReplayButton.interactable = false;
                }
                else
                    WinReplayButton.interactable = true;
            }
            else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
            {
                LostPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    LostReplayButton.interactable = false;
                }
                else
                    LostReplayButton.interactable = true;
            }
        }
    }
    void TimerEnd()
    {
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"])
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
            {
                WinPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    WinReplayButton.interactable = false;
                }
                else
                    WinReplayButton.interactable = true;
            }
            else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
            {
                LostPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    LostReplayButton.interactable = false;
                }
                else
                    LostReplayButton.interactable = true;
            }
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"])
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
            {
                LostPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    LostReplayButton.interactable = false;
                }
                else
                    LostReplayButton.interactable = true;
            }
            else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
            {
                WinPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    WinReplayButton.interactable = false;
                }
                else
                    WinReplayButton.interactable = true;
            }
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] )
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
            {
                DrawPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    DrawReplayButton.interactable = false;
                }
                else
                    DrawReplayButton.interactable = true;
            }
            else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
            {
                DrawPlayAgainButton.SetActive(true);

                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    DrawReplayButton.interactable = false;
                }
                else
                    DrawReplayButton.interactable = true;
            }
        }
    }
}
