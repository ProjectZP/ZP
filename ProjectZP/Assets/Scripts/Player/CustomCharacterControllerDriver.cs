using UnityEngine.XR.Interaction.Toolkit;

public class CustomCharacterControllerDriver : CharacterControllerDriver
{
    private new void Awake()
    {
        base.Awake();

        // if min/max height is not set
        if (minHeight == 0f)
            minHeight = 1.3f;
        if (maxHeight == float.PositiveInfinity)
            maxHeight = 2.6f;

    }
    private void Update()
    {
        UpdateCharacterController();
    }
}
