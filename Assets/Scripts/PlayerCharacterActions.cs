using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class PlayerCharacterActions : MonoBehaviour
{
    public CoreBody body;
    public short jumpStrength = 100;

    public void Awake()
    {
        EventBus.Register<InputButtonArgs>(EnumHelper.GetStringID(CustomHook.INPUT_BUTTON), OnInputButton);
    }

    public void OnInputButton(InputButtonArgs args)
    {
        //Debug.Log($"c# script: receieved args: {args}");
        switch(args.action)
        {
            case CustomInputAction.JUMP:
                switch(args.phase)
                {
                    case InputActionPhase.Started:
                        Debug.Log("PRESSED");
                        body.TriggerY(jumpStrength);
                        break;
                }
                break;
        }
    }
}
