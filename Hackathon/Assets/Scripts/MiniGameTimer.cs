using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTimer : MonoBehaviour
{
    //cooldown time in seconds
    public float cooldownDuration = 30f;

    private bool isCooldownActive = false;

    private float cooldownTimer = 0f;

    public bool IsCooldownActive
    {
        get
        {
            return isCooldownActive;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isCooldownActive)
        {
            cooldownTimer -= Time.deltaTime;
            Debug.Log("Cooldown is Active");
            Debug.Log(cooldownTimer);

            if (cooldownTimer <= 0f)
            {
                Debug.Log("Cooldown is Disabled");
                isCooldownActive = false;
            }
        }
    }

    public void StartCooldown()
    {
        isCooldownActive = true;
        cooldownTimer = cooldownDuration;
    }
}
