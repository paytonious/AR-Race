using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectImageMode : MonoBehaviour
{
  [SerializeField] EditFrameMode editFrame;
  [SerializeField] AddFrameMode addFrame;

  public bool isReplacing = false;

  void OnEnable() 
  {
    UIController.ShowUI("SelectImage");
  }

  public void ImageSelected(ImageInfo image) 
  {
    if (isReplacing) 
    {
      editFrame.currentFrame.SelectImage(image);
      InteractionController.EnableMode("EditFrame");
    }
    else 
    {
      addFrame.ImageInfo = image;
      InteractionController.EnableMode("AddFrame");
    }
  }

}
