using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    //Attach this script to each individual Minimap Icon
    public class MinimapIcon : MonoBehaviour
    {
        private Vector3 baseScale;

        private void Start()
        {
            baseScale = transform.localScale;
            MinimapCamera.OnZoomChanged += Minimap_OnZoomChanged;
        }

            private void Minimap_OnZoomChanged(object sender, System.EventArgs e)
            {
                transform.localScale = baseScale * MinimapCamera.GetZoom() / 180f;
            }
        

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }

}
