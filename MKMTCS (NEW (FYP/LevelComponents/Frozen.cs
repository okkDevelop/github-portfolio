using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Frozen : MonoBehaviour
{
    [SerializeField] private float affectedSpeed;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Debuff>()?.AffectSpeed(affectedSpeed);
    }
}
