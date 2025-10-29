using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
	[SerializeField] private GameObject lecturer_jacob;
	[SerializeField] private GameObject lecturer_matthew;
	[SerializeField] private GameObject lecturer_lai;
	[SerializeField] private GameObject lecturer_puja;
	[SerializeField] private GameObject student_imam;
	[SerializeField] private GameObject student_ozk;
	[SerializeField] private GameObject student_nsk;
	[SerializeField] private GameObject student_ahmad;
	
	[SerializeField] private GameObject studentSelectionPanel;
	[SerializeField] private GameObject lecturerSelectionPanel;
	
	[SerializeField] private GameObject lecturerSpawnArea;
	[SerializeField] private GameObject studentSpawnArea;
    [SerializeField] private LayerMask obstacleLayer;
	
	private Bounds lecturerSpawnBounds;
	private Bounds studentSpawnBounds;
	
	private void Start()
    {
        // Get the collider component from lecturerSpawnArea and calculate its bounds
        if (lecturerSpawnArea != null)
        {
            Collider lecturerCollider = lecturerSpawnArea.GetComponent<Collider>();
            if (lecturerCollider != null)
            {
                lecturerSpawnBounds = lecturerCollider.bounds;
            }
            else
            {
                Debug.LogWarning("No collider found on lecturerSpawnArea GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("lecturerSpawnArea GameObject is not assigned.");
        }
		if (studentSpawnArea != null)
        {
            Collider studentCollider = studentSpawnArea.GetComponent<Collider>();
            if (studentCollider != null)
            {
                studentSpawnBounds = studentCollider.bounds;
            }
            else
            {
                Debug.LogWarning("No collider found on studentSpawnArea GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("studentSpawnArea GameObject is not assigned.");
        }
		
    }

	
    public void SpawnJacob()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			// Get random position within the bounds of the box collider
			Vector3 randomPosition = GetRandomLecturerSpawnPosition();
			CloseLecturerSelectionPanel();
			// Instantiate the object at the random position
			PhotonNetwork.Instantiate(lecturer_jacob.name, randomPosition, Quaternion.identity);
		}
	}
	
	public void SpawnMatthew()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			Vector3 randomPosition = GetRandomLecturerSpawnPosition();
			CloseLecturerSelectionPanel();
			PhotonNetwork.Instantiate(lecturer_matthew.name, randomPosition, Quaternion.identity);
		}
	}
	
	public void SpawnLai()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			Vector3 randomPosition = GetRandomLecturerSpawnPosition();
			CloseLecturerSelectionPanel();
			PhotonNetwork.Instantiate(lecturer_lai.name, randomPosition, Quaternion.identity);
		}
	}
	
	public void SpawnPuja()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			Vector3 randomPosition = GetRandomLecturerSpawnPosition();
			CloseLecturerSelectionPanel();
			PhotonNetwork.Instantiate(lecturer_puja.name, randomPosition, Quaternion.identity);
		}
	}
	
	public void SpawnImam()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			Vector3 randomPosition = GetRandomStudentSpawnPosition();
			CloseStudentSelectionPanel();
			PhotonNetwork.Instantiate(student_imam.name, randomPosition, Quaternion.identity);
		}
	}
	
	public void SpawnOzk()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			Vector3 randomPosition = GetRandomStudentSpawnPosition();
			CloseStudentSelectionPanel();
			PhotonNetwork.Instantiate(student_ozk.name, randomPosition, Quaternion.identity);
		}
	}
	
	public void SpawnNsk()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			Vector3 randomPosition = GetRandomStudentSpawnPosition();
			CloseStudentSelectionPanel();
			PhotonNetwork.Instantiate(student_nsk.name, randomPosition, Quaternion.identity);
		}
	}
	
	public void SpawnAhmad()
	{
		if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer != null)
        {

			Vector3 randomPosition = GetRandomStudentSpawnPosition();
			CloseStudentSelectionPanel();
			PhotonNetwork.Instantiate(student_ahmad.name, randomPosition, Quaternion.identity);
		}
	}
	
	public Vector3 GetRandomLecturerSpawnPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        int attempts = 0;
        while (attempts < 10) // Try at most 10 times to find a valid position
        {
            randomPosition = new Vector3(
                Random.Range(lecturerSpawnBounds.min.x, lecturerSpawnBounds.max.x),
                Random.Range(lecturerSpawnBounds.min.y, lecturerSpawnBounds.max.y),
                Random.Range(lecturerSpawnBounds.min.z, lecturerSpawnBounds.max.z));

            if (!IsPositionOverlapping(randomPosition))
            {
                return randomPosition;
            }
            attempts++;
        }
        Debug.LogWarning("Failed to find a valid spawn position!");
        return randomPosition;
    }
	
	public Vector3 GetRandomStudentSpawnPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        int attempts = 0;
        while (attempts < 10) // Try at most 10 times to find a valid position
        {
            randomPosition = new Vector3(
                Random.Range(studentSpawnBounds.min.x, studentSpawnBounds.max.x),
                Random.Range(studentSpawnBounds.min.y, studentSpawnBounds.max.y),
                Random.Range(studentSpawnBounds.min.z, studentSpawnBounds.max.z));

            if (!IsPositionOverlapping(randomPosition))
            {
                return randomPosition;
            }
            attempts++;
        }
        Debug.LogWarning("Failed to find a valid spawn position!");
        return randomPosition;
    }
	
    private bool IsPositionOverlapping(Vector3 position)
    {
        // Check if there's any obstacle (wall) at the given position
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, Vector2.one, 0f, obstacleLayer);
        return colliders.Length > 0;
    }
	
	private void CloseStudentSelectionPanel()
	{
		studentSelectionPanel.SetActive(false);
	}
	
	private void CloseLecturerSelectionPanel()
	{
		lecturerSelectionPanel.SetActive(false);
	}
}
