using UnityEngine;
using Unity.VisualScripting;

public class VisualScriptingCustomUnit : Unit
{
    public ControlInput input0;

    public ControlOutput output0;

    protected override void Definition()
    {
        input0 = ControlInput("input", InputTriggered);

        output0 = ControlOutput("output0");
        Timer t;
    }

    private ControlOutput InputTriggered(Flow flow)
    {
        Debug.LogFormat("zvp: custom input event triggered");
        return output0;
    }
}
