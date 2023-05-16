using UnityEngine;
using UnityEngine.UI;
using EmotivUnityPlugin;

namespace dirox.emotiv.controller
{
  public class ContactQualityNodeView3D
  {
    private GameObject display;
    private Color[] colors;
/*    private Renderer mr;*/

    public ContactQualityNodeView3D SetDisplay(GameObject display)
    {
      this.display = display;
      return this;
    }

    public ContactQualityNodeView3D SetColors(Color[] colors)
    {
      this.colors = colors;
      return this;
    }

    /// <summary>
    /// Call this to set the quality color for the corresponding nodes
    /// </summary>
    /// <param name="quality">Quality.</param>
    public void SetQuality(ContactQualityValue quality)
    {
      if (display != null)
      {
        int ordinal = (int)quality;
        if (ordinal > colors.Length - 1)
          ordinal = colors.Length - 1;

        display.GetComponent<Renderer>().material.color = colors[ordinal];
      }
    }
  }
}