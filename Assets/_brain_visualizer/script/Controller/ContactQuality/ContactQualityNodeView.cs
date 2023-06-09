using UnityEngine;
using UnityEngine.UI;
using EmotivUnityPlugin;

namespace dirox.emotiv.controller
{
    public class ContactQualityNodeView
    {
        private Image display;
        private GameObject gameObject;
        private Color[] colors;

        public ContactQualityNodeView SetDisplay (Image display)
        {
            this.display = display;
            return this;
        }

        public ContactQualityNodeView SetColors (Color[] colors)
        {
            this.colors = colors;
            return this;
        }

        public ContactQualityNodeView SetGameObject(GameObject gameObject)
        {
          this.gameObject = gameObject;
          return this;
        }

        public ContactQualityNodeView SetGameObjectColors(Color[] colors)
        {
          this.colors = colors;
          return this;
        }

    /// <summary>
    /// Call this to set the quality color for the corresponding nodes
    /// </summary>
    /// <param name="quality">Quality.</param>
    public void SetQuality (ContactQualityValue quality)
        {
            if (display != null) {
                int ordinal = (int)quality;
                if (ordinal > colors.Length - 1)
                    ordinal = colors.Length - 1;
                display.color = colors [ordinal];
            }
        }

      /// <summary>
      /// Call this to set the quality color for the corresponding nodes
      /// </summary>
      /// <param name="quality">Quality.</param>
      public void SetGOQuality(ContactQualityValue quality)
      {
      Debug.Log("in GO");
      if (gameObject != null)
      {
        Debug.Log("setting GO quality");
        Debug.Log(gameObject);
        int ordinal = (int)quality;


        if (gameObject.GetComponent<Light>()) { 
          gameObject.GetComponent<Light>().color = new Color(
            colors[ordinal].r,
            colors[ordinal].g,
            colors[ordinal].b
            );
        } else
        {
          gameObject.GetComponent<Renderer>().material.color = new Color(
            colors[ordinal].r,
            colors[ordinal].g,
            colors[ordinal].b
            );
        }
      }
    }
  }
}
