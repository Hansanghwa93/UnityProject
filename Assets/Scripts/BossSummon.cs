using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummon : MonoBehaviour
{
    public GameObject boss;

    void Start()
    {
        Instantiate(boss);
    }
}
