using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAbility : MonoBehaviour, IHaveCooldown
{
    [SerializeField] private GameObject helicopterPrefab;

    [SerializeField] private RectTransform canvasMain;
    [SerializeField] private RectTransform playerZone;
    [SerializeField] private RectTransform helicopterDeathZone;

    [SerializeField] private int helicopterSpawnDistance;

    [SerializeField] private AudioClip helicopterSound;
    [SerializeField] private float audioDuration;

    [SerializeField] int iD;
    [SerializeField] float cD;
    [SerializeField] CoolDownSystem coolDownSystem;

    public int CoolDownId => iD;
    public float CoolDownDuration => cD;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            UseHelicopter(STRAFE_DIRECTIONS.RIGHT);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseHelicopter(STRAFE_DIRECTIONS.LEFT);
        }
    }

    private void UseHelicopter(STRAFE_DIRECTIONS direction)
    {
        if (coolDownSystem.IsOnCoolDown(iD)) return;

        coolDownSystem.PutOnCooldown(this);
        //when character looks towards mousePos transform.right its forward vector2 so, +up is left, -up is right
        //spawn helicopter depending on direction

        //loop audio
        SoundManager.Instance.LoopAudio(helicopterSound, audioDuration);

        switch (direction)
        {
            case STRAFE_DIRECTIONS.LEFT:
                FindPointToSpawnInGivenDirection(-transform.up);
                break;

            case STRAFE_DIRECTIONS.RIGHT:
                FindPointToSpawnInGivenDirection(transform.up);
                break;
        }
        
        //put it on coolDown
    }

    void FindPointToSpawnInGivenDirection(Vector2 direction)
    {
        StartCoroutine(FindPointAndSpawn(direction));
    }

    IEnumerator FindPointAndSpawn(Vector3 direction)
    {
        var checkPoint = transform.position + direction * helicopterSpawnDistance;
        yield return null;

        //spawn and setup Helicopter

        //TODO poco????just need constructor
        GameObject helicopter = Instantiate(helicopterPrefab, checkPoint, Quaternion.identity);
        var helicopterScript =  helicopter.GetComponent<Helicopter>();
        helicopterScript.player = transform;
        helicopterScript.playerZone = playerZone;
        helicopterScript.helicopterDeathZone = helicopterDeathZone;
    }
/*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + newDir);
    }*/

    public enum STRAFE_DIRECTIONS
    {
        LEFT,
        RIGHT
    }
}
