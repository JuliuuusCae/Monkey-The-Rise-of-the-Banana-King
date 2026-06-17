using UnityEngine;


[CreateAssetMenu(menuName = "RPG Setup/Default Stat Setup", fileName = "Default Stat Setup")]
public class Stat_SetupSO : ScriptableObject
{
    [Header("Resource")]
    public float maxHealth = 100;
    public float healthRegen;

    [Header("Offense")]
    public float attackSpeed = 1;
    public float damage = 10;
    public float critChance;
    public float critPower = 150;
    public float armorReduction;

    [Header("Defense")]
    public float armor;
    public float evasion;

    [Header("Major")]
    public float strength;
    public float agility;
    public float intelligence;
    public float vitality;
}
