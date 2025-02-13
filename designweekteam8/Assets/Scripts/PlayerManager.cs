using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> spawnLocations;
    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager inputManager;

    private void Awake()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        inputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        inputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        Transform playerParent = player.transform.parent;
        playerParent.position = spawnLocations[players.Count - 1].position;
    }
}
