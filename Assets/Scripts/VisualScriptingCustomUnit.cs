using UnityEngine;
using Unity.VisualScripting;

public class VisualScriptingCustomUnit : Unit
{
    public ValueInput data0;

    public ControlInput input0;

    public ControlOutput output0;

    protected override void Definition()
    {
        data0 = ValueInput<int>("data0", 0);

        input0 = ControlInput("input", InputTriggered);
        output0 = ControlOutput("output0");
        //Timer t;
    }

    private ControlOutput InputTriggered(Flow flow)
    {
        Debug.LogFormat("zvp: custom input event triggered: {0}", flow.GetValue<int>(data0));
        return output0;
    }
}
