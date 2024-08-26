using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MechanicManager : MonoBehaviour
{
    [SerializeField] private GameObject WinPlayAgainButton;
    [SerializeField] private GameObject LostPlayAgainButton;
    [SerializeField] private Button WinReplayButton;
    [SerializeField] private Button LostReplayButton;
    //[SerializeField] private Timer timer;
    public bool isEnd;
    //private bool startCheck;

    // Start is called before the first frame update
    private void Start()
    {
        WinPlayAgainButton.SetActive(false);
        LostPlayAgainButton.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        StartCoroutine(WinAndLost());
    }

    private IEnumerator WinAndLost()
    {
        yield return new WaitForSeconds(20f);
        //int studentLayer = LayerMask.NameToLayer("Student");
        //int lecturerLayer = LayerMask.NameToLayer("Lecturer");

        GameObject[] studentPlayer = GameObject.FindGameObjectsWithTag("Student");
        GameObject[] lecturerPlayer = GameObject.FindGameObjectsWithTag("Lecturer");

        Debug.Log(studentPlayer);
        Debug.Log(lecturerPlayer);

        if (studentPlayer.Length <= 0 && lecturerPlayer.Length > 0)
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
            isEnd = true;
        }
        else if (lecturerPlayer.Length <= 0 && studentPlayer.Length > 0)
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
            isEnd = true;
        }
        //else if (studentPlayer.Length > lecturerPlayer.Length)
        //{
        //    if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
        //    {
        //        WinPlayAgainButton.SetActive(true);

        //        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        //        {
        //            WinReplayButton.interactable = false;
        //        }
        //        else
        //            WinReplayButton.interactable = true;
        //    }
        //    else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
        //    {
        //        LostPlayAgainButton.SetActive(true);

        //        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        //        {
        //            LostReplayButton.interactable = false;
        //        }
        //        else
        //            LostReplayButton.interactable = true;
        //    }
        //    isEnd = true;
        //}
        //else if (lecturerPlayer.Length > studentPlayer.Length)
        //{
        //    if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(2))
        //    {
        //        WinPlayAgainButton.SetActive(true);

        //        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        //        {
        //            WinReplayButton.interactable = false;
        //        }
        //        else
        //            WinReplayButton.interactable = true;
        //    }
        //    else if (PhotonNetwork.LocalPlayer.CustomProperties["TeamInfo"].Equals(1))
        //    {
        //        LostPlayAgainButton.SetActive(true);

        //        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        //        {
        //            LostReplayButton.interactable = false;
        //        }
        //        else
        //            LostReplayButton.interactable = true;
        //    }
        //    isEnd = true;
        //}
        else
        {
            Debug.Log(studentPlayer.Length);
            Debug.Log(lecturerPlayer.Length);
        }
    }

}
