using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravestone : MonoBehaviour
{

    private void Update()
    {
        if (GetComponent<Health>().GetGettingDamaged()) { GettingDamaged(); }
    }

    private void GettingDamaged()
    {
        GetComponent<Animator>().SetTrigger("isDamaged");
    }
}
