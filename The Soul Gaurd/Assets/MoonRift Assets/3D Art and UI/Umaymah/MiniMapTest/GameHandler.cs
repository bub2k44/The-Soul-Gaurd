using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimap;
public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Minimap.MinimapClass.Init();
        /**CMDebug.ButtonUI(new Vector2(300, 200), "Show Minimap", MinimapWindow.Show);
        CMDebug.ButtonUI(new Vector2(300, 160), "Hide Minimap", MinimapWindow.Hide);

        
        CMDebug.ButtonUI(new Vector2(300, 20), "Zoom In", MinimapWindow.ZoomIn);
        CMDebug.ButtonUI(new Vector2(300, -20), "Zoom Out", MinimapWindow.ZoomOut); 

         ButtonUI(new Vector2(300, 200), "Show Minimap", MinimapWindow.Show);
        ButtonUI(new Vector2(300, 160), "Hide Minimap", MinimapWindow.Hide);

        
        ButtonUI(new Vector2(300, 20), "Zoom In", MinimapWindow.ZoomIn);
        ButtonUI(new Vector2(300, -20), "Zoom Out", MinimapWindow.ZoomOut); **/


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            MinimapWindow.Show();
        }
         if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            MinimapWindow.Hide();
        }


        if(Input.GetKeyDown("i"))
        {
            MinimapCamera.ZoomIn();
        }
         if (Input.GetKeyDown("o"))
        {
            MinimapCamera.ZoomOut();
        }      
    } 
     
}
