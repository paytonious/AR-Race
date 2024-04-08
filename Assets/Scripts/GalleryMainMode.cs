using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GalleryMainMode : MonoBehaviour
{
    [SerializeField] EditFrameMode editMode;
    [SerializeField] SelectImageMode selectImage;

    Camera camera;

    void Start()
    {
        camera = Camera.main;
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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) 
        {
            FramedPhoto frame = hit.collider.GetComponentInParent<FramedPhoto>();
            editMode.currentFrame = frame;
            InteractionController.EnableMode("EditFrame");
        }
    }

    public void SelectImageToAdd()
    {
        selectImage.isReplacing = false;
        InteractionController.EnableMode("SelectImage");
    }

    void OnEnable()
    {
        UIController.ShowUI("Main");
    }
}
