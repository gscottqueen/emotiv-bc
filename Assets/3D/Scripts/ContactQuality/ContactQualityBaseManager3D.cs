using UnityEngine;
using Zenject;
using EmotivUnityPlugin;

namespace dirox.emotiv.controller
{
  public class ContactQualityBaseManager3D : MonoBehaviour
  {
    [Inject]
    public void InjectDependencies3D(ContactQualityColorSetting colorSettings)
    {
      SetColorSettingForNodes(colorSettings.colors);
    }

    protected virtual void SetColorSettingForNodes(Color[] allColors)
    {
    }

    public virtual void SetContactQualityColor(ContactQualityValue[] contacts)
    {
    }
  }
}