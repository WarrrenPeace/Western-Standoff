using UnityEngine;

[CreateAssetMenu(fileName = "QuickTimeEvent", menuName = "Scriptable Objects/QuickTimeEvent")]
public class QuickTimeEventObject : ScriptableObject
{
    public float speed; //1 is really fast LMAO
    public Vector2 min_Max_For_SafeZone; //Min in max of safezone
}
