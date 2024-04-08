using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FramedPhoto : MonoBehaviour
{
    [SerializeField] Transform scalerObject;
    [SerializeField] GameObject imageObject;
    [SerializeField] GameObject highlightObject;
    [SerializeField] Collider boundingCollider;

    int layer;
    bool isEditing;
    ImageInfo imageInfo;

    ARRaycastManager raycaster;
    Camera mainCamera;

//TODO move in all stuff from Move and Resize scripts

    public void Awake() {
      Highlight(false);
      layer = LayerMask.NameToLayer("PlacedObjects");
    }
    
    public void Start() {

    }

    public void SetImage(ImageInfo image)
    {
        imageInfo = image;
        
        Renderer renderer = imageObject.GetComponent<Renderer>();
        Material material = renderer.material;
        material.SetTexture("_MainTex", imageInfo.texture);
        AdjustScale();
    }

    public void SelectImage()
    {

    }

    public void AdjustScale()
    {
        Vector2 scale = ImagesData.AspectRatio(imageInfo.width, imageInfo.height);
        scalerObject.localScale = new Vector3(scale.x, scale.y, 1f);
    }

    public void Highlight(bool show) {
      if (highlightObject) {
        highlightObject.SetActive(show);
      }
    }

    public void BeingEdited(bool editing) {
      Highlight(editing);
      isEditing = editing;
    }

    void OnTriggerStay(Collider other) 
    {
      const float spacing = 0.1f;
      if (!isEditing && other.gameObject.layer == layer) 
      {
        Bounds bounds = boundingCollider.bounds;
        if (other.bounds.Intersects(bounds)) {
          Vector3 centerDistance = bounds.center - other.bounds.center; //if both 0 then choose one of the two and move it a predetermined direction
          Vector3 distOnPlane = Vector3.ProjectOnPlane(centerDistance, transform.forward);
          Vector3 direction = distOnPlane.normalized;
          float distanceToMoveThisFrame = bounds.size.x * spacing;
          transform.Translate(direction * distanceToMoveThisFrame);
        }
      }
    }

    public void OnMoveObject(InputValue value)
    {
      if (!enabled) return;
      if (!isEditing) return;
      if (EventSystem.current.IsPointerOverGameObject(0)) return;

      //more here to check and call MoveObject below
    }

    void MoveObject(Vector2 touchPosition)
    {
      Ray ray = mainCamera.ScreenPointToRay(touchPosition);
      //more here
    }

    public void OnResizeObject()
    {

    }

    void ResizeObject()
    {

    }
    
}