using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHp;
    public int hp;
    public int maxMp;
    public int mp;
    public int damage;
    public int armor;
    public int Str;
    public int Dex;
    public int Int;
    public int Luk;

    private void Awake()
    {
        hp = maxHp;
        mp = maxMp;
    }

}
