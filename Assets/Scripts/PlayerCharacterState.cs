using UnityEngine;

public class PlayerCharacterState : MonoBehaviour
{
    public enum State
    {
        NONE,
        WALL_CLING
    }

    public State state;
}
