using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PercentageOfScreen : MonoBehaviour
{
    enum PositionEnum { LEFT, MIDDLE, RIGHT } //Position of the sprites
    enum OrientationEnum { HORIZONTAL, VERTICAL } //Orientation of the panel
    enum PlacementEnum { TOP, BOTTOM, LEFT, RIGHT } //Placement of the panel


    [SerializeField] private float HorizontalPercentage;
    [SerializeField] private float VerticalPercentage;
    [SerializeField] private PositionEnum spritePosition;
    [SerializeField] private OrientationEnum panelOrientation;
    [SerializeField] private PlacementEnum panelPlacement;



    private RectTransform _PanelImage;
    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _PanelImage = GetComponent<RectTransform>();

        //Debug.Log("HP: " + HorizontalPercentage);
        //Debug.Log("VP: " + VerticalPercentage);
        //Debug.Log("HP/100: " + (HorizontalPercentage / 100));
        //Debug.Log("VP/100: " + (VerticalPercentage / 100));
        //Debug.Log("Camera: " + _mainCamera.pixelWidth + " * " + _mainCamera.pixelHeight);


        float newWidth = (HorizontalPercentage / 100) * _mainCamera.pixelWidth;
        float newHeight = (VerticalPercentage / 100) * _mainCamera.pixelHeight;

        //Debug.Log(gameObject + " : " + newWidth + " * " + newHeight);

        _PanelImage.sizeDelta = new Vector2(newWidth, newHeight);


        if (panelOrientation.Equals(OrientationEnum.HORIZONTAL)){ //If the whole panel is horizontal
            if (panelPlacement.Equals(PlacementEnum.TOP)) //If the whole panel is placed on top of the screen
            {
                _PanelImage.anchoredPosition = new Vector2(_PanelImage.anchoredPosition.x, -newHeight / 2);
            }
            else if (panelPlacement.Equals(PlacementEnum.BOTTOM))
            {
                _PanelImage.anchoredPosition = new Vector2(_PanelImage.anchoredPosition.x, newHeight / 2);
            }

            //Positions of the side sprites

            if (spritePosition.Equals(PositionEnum.LEFT))
            {
                _PanelImage.anchoredPosition = new Vector2(newHeight / 2, _PanelImage.anchoredPosition.y);
            }
            else if (spritePosition.Equals(PositionEnum.RIGHT))
            {
                _PanelImage.anchoredPosition = new Vector2(-newHeight / 2, _PanelImage.anchoredPosition.y);
            }
        }

        GetComponent<UI_Offset>().OffsetElement(_PanelImage);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
