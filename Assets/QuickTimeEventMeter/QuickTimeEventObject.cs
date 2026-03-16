using UnityEngine;

[CreateAssetMenu(fileName = "QuickTimeEvent", menuName = "Scriptable Objects/QuickTimeEvent")]
public class QuickTimeEventObject : ScriptableObject
{
    public float speed; //1 is really fast LMAO
    public Vector2 min_Max_For_SafeZone; //Min in max of safezone
    public string Character; //Name of person in front of you
    public string Dialogue; //What text appears right before facing this character
    public string Description; //What text appears right before facing this character
}
