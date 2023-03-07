using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class PlayerCharacterActions : MonoBehaviour
{
    public CoreBody body;
    public short jumpStrength = 100;
    public short groundMoveSpeed = 100;

    public void Awake()
    {
        EventBus.Register<InputButtonArgs>(InputButtonEvent.Hook, OnInputButton);
        EventBus.Register<InputAxis1DArgs>(InputAxis1DEvent.Hook, OnInputAxis1D);
    }

    public void OnInputButton(InputButtonArgs args)
    {
        switch(args.action)
        {
            case CustomInputAction.JUMP:
                switch(args.phase)
                {
                    case InputActionPhase.Started:
                        body.TriggerY(jumpStrength);
                        break;
                }
                break;
        }
    }

    public void OnInputAxis1D(InputAxis1DArgs args)
    {
        switch (args.action)
        {
            case (CustomInputAction.MOVE):
                body.TriggerX((short)(groundMoveSpeed * args.axis));
                break;
        }
    }
}
