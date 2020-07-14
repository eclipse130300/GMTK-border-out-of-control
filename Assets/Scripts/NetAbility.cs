using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetAbility : MonoBehaviour, IHaveCooldown
{
    [SerializeField] GameObject netPrefab;
    [SerializeField] Transform parentTransform;
    [SerializeField] float netDuration;

    [SerializeField] AudioClip netClip;

    [SerializeField] int netAbilityId;
    [SerializeField] float cD;
    [SerializeField] CoolDownSystem coolDownSystem;

    public int CoolDownId => netAbilityId;
    public float CoolDownDuration => cD;

    public void PlaceNet()
    {
        var netGO = Instantiate(netPrefab, transform.position, Quaternion.Euler(transform.eulerAngles), parentTransform);
        //play audio
        SoundManager.Instance.PlayClipAtPoint(netClip, transform.position);

        //setup net
        var net = netGO.GetComponent<Net>();
        net.coolDownSystem = coolDownSystem;
        net.CoolDownId = net.GetInstanceID();
        net.CoolDownDuration = netDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !coolDownSystem.IsOnCoolDown(netAbilityId))
        {
            PlaceNet();
            coolDownSystem.PutOnCooldown(this);
/*            Debug.Log("I PLACE NET! ID : " + netAbilityId);*/
        }
    }
}
