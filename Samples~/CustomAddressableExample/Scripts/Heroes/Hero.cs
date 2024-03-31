using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "ScriptableObjects/Hero")]
public class Hero : ScriptableObject
{
    public Sprite Icon;

    public float Attack;
    public float Health;
    public float Speed;
    public float Defense;
}
