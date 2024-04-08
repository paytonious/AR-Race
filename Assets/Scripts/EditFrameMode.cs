using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditFrameMode : MonoBehaviour
{
    [SerializeField] SelectImageMode selectImage;
    public FramedPhoto currentFrame;
    Camera camera;

    void Start() {
      camera = Camera.main;
    }

    void OnEnable()
    {
      UIController.ShowUI("EditFrame");   

      if(currentFrame) {
        currentFrame.BeingEdited(true);
      }
    }

    void onDisable() {
      if(currentFrame) {
        currentFrame.BeingEdited(false);
      }
    }

    public void OnSelectObject(InputValue value)
    {
      Vector2 touchPosition = value.Get<Vector2>();
      FindObjectToEdit(touchPosition);
    }

    void FindObjectToEdit(Vector2 touchPosition)
    {
      Ray ray = camera.ScreenPointToRay(touchPosition);
      RaycastHit hit;
      int layerMask = 1 << LayerMask.NameToLayer("PlacedObjects");
      
      if (Physics.Raycast(ray, out hit, 50f, layerMask))
      {
        if (hit.collider.gameObject != currentFrame.gameObject)
        {
          currentFrame.BeingEdited(false);
          FramedPhoto frame = hit.collider.GetComponentInParent<FramedPhoto>();
          currentFrame = frame;
          frame.BeingEdited(true);
        }
      }
    }

    public void DeletePicture() {
      GameObject.Destroy(currentFrame.gameObject);
      InteractionController.EnableMode("Main");
    }

    public void SelectImageToReplace() {
      selectImage.isReplacing = true;
      InteractionController.EnableMode("SelectImage");
    }
}
