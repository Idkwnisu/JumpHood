using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager _instance;

    public GameObject player;

    private CharacterScript controller;

    public bool overrideDirection;

    public Vector3 newDirection;

    public static CheckpointManager Instance { get { return _instance; } }

    public Checkpoint lastCheckpoint;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        controller = player.GetComponent<CharacterScript>();
    }

    public void setNewCheckpoint(Checkpoint newCheckpoint, bool overrideDir, Vector3 newDir)
    {
        lastCheckpoint = newCheckpoint;
        overrideDirection = overrideDir;
        newDirection = newDir;
    }

    public void resetPlayer()
    {
        if(lastCheckpoint != null)
        {
            controller.KillCharacter();
            Invoke("TeleportPlayer", 1.0f);
            Invoke("RespawnPlayer", 2.0f);
        }
    }

    public void TeleportPlayer()
    {
        player.transform.position = lastCheckpoint.respawnPoint.position;
        player.GetComponent<CharacterScript>().verticalMultiplier = 1.0f;
        player.GetComponentInChildren<SpriteRenderer>().flipY = false;
        if (overrideDirection)
        {
            player.GetComponent<CharacterScript>().movementDirection = newDirection;
        }
        controller.PlayParticle();
    }

    public void RespawnPlayer()
    {
        controller.ResurrectCharacter();
        AudioManager.Instance.PlayAudioClue("Respawn");
    }
}
