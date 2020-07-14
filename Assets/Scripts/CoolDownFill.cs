using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownFill : MonoBehaviour
{
    [Header("Id has to be the same as ID of IHaveCooldown item")]
    public int iD;

    private Image image;
    public CoolDownSystem coolDownSystem;
    public bool isOnCooldown;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(coolDownSystem.IsOnCoolDown(iD) && !isOnCooldown)
        {
            image.fillAmount = 1f;
            isOnCooldown = true;
        }

        if(isOnCooldown)
        {
            image.fillAmount = coolDownSystem.GetNormilizedTime(iD);
            /*Debug.Log(coolDownSystem.GetNormilizedTime(iD));*/
        }
    }

    
}
