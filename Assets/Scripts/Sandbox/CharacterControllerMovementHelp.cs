using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine;


/*"CharacterControllerMovementHelp" script is responsible for updating the height and center of the CharacterController
 component based on the position of the camera.
 It ensures that the character controller's height and center are adjusted according to the camera's 
 position in the XR (Extended Reality) space.*/
public class CharacterControllerMovementHelp : MonoBehaviour
{
    private XROrigin xrRig;
    private CharacterController charControl;
    private CharacterControllerDriver driver;

    void Start()
    {
        xrRig = GetComponent<XROrigin>();
        charControl = GetComponent<CharacterController>();
        driver = GetComponent<CharacterControllerDriver>();
    }

    void Update()
    {   
        UpdateCharacterController();
    }

    /// <summary>
    /// Updates the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
    /// based on the camera's position.
    /// </summary>
    protected virtual void UpdateCharacterController()
    {
        if (xrRig == null || charControl == null)
            return;

        var height = Mathf.Clamp(xrRig.CameraInOriginSpaceHeight, driver.minHeight, driver.maxHeight);

        Vector3 center = xrRig.CameraInOriginSpacePos;
        center.y = height / 2f + charControl.skinWidth;

        charControl.height = height;
        charControl.center = center;
    }
}
