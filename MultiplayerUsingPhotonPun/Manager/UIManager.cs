using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class UIManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void OnPlayerAgainBtnClicked() 
    {
        if (SceneManager.GetActiveScene().name == "Factory")
        {
            ResetUI();
            PhotonNetwork.LoadLevel("de_dust");
        }
        else if (SceneManager.GetActiveScene().name == "de_dust")
        {
            ResetUI();
            PhotonNetwork.LoadLevel("de_nuke");
        }
        else if (SceneManager.GetActiveScene().name == "de_nuke")
        {
            ResetUI();
            PhotonNetwork.LoadLevel("Factory");
        }
        else if (SceneManager.GetActiveScene().name == "Factory_LTSM") 
        {
            ResetUI();
            PhotonNetwork.LoadLevel("de_dust_LTSM");
        }
        else if (SceneManager.GetActiveScene().name == "de_dust_LTSM")
        {
            ResetUI();
            PhotonNetwork.LoadLevel("de_nuke_LTSM");
        }
        else if (SceneManager.GetActiveScene().name == "de_nuke_LTSM")
        {
            ResetUI();
            PhotonNetwork.LoadLevel("Factory_LTSM");
        }
    }

    public void OnQuitBtnClicked() 
    {
        Application.Quit();
    }

    void ResetUI()
    {
        PhotonNetwork.CurrentRoom.CustomProperties["LecturerScore"] = 0;
        PhotonNetwork.CurrentRoom.CustomProperties["StudentScore"] = 0;
        Hashtable setTime = new Hashtable();
        setTime["Timer"] = 180;
        PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
    }
    //[PunRPC]
    //private void Replay()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    PhotonNetwork.LoadLevel(currentSceneIndex);
    //}
}
