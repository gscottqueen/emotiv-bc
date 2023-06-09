using UnityEngine;
using UnityEngine.UI;
using Zenject;
using EmotivUnityPlugin;

namespace dirox.emotiv.controller
{
    public class ContactQualityInsightManager : ContactQualityBaseManager
    {

    // GameObjects
    [SerializeField] GameObject AF3GO;
    [SerializeField] GameObject T7GO;
    [SerializeField] GameObject PZGO;
    [SerializeField] GameObject T8GO;
    [SerializeField] GameObject AF4GO;
    [SerializeField] GameObject CMSGO;

    // inject Ids
    [Inject(Id = "AF3")] ContactQualityNodeView AF3GOView;
    [Inject(Id = "T7")] ContactQualityNodeView T7GOView;
    [Inject(Id = "PZ")] ContactQualityNodeView PZGOView;
    [Inject(Id = "T8")] ContactQualityNodeView T8GOView;
    [Inject(Id = "AF4")] ContactQualityNodeView AF4GOView;
    [Inject(Id = "CMS")] ContactQualityNodeView CMSGOView;

    protected override void SetColorSettingForNodes (Color[] allColors)
        {

            // set colors for nodes
            AF3GOView.SetGameObjectColors(allColors).SetGameObject(AF3GO);
            T7GOView.SetGameObjectColors(allColors).SetGameObject(T7GO);
            PZGOView.SetGameObjectColors(allColors).SetGameObject(PZGO);
            T8GOView.SetGameObjectColors(allColors).SetGameObject(T8GO);
            AF4GOView.SetGameObjectColors(allColors).SetGameObject(AF4GO);
            CMSGOView.SetGameObjectColors(allColors).SetGameObject(CMSGO);
    }

    public override void SetContactQualityColor(ContactQualityValue[] contacts)
    {
      if (contacts == null)
      {

        // GameObjects
        AF3GOView.SetGameObject(AF3GO).SetGOQuality(ContactQualityValue.NO_SIGNAL);
        T7GOView.SetGameObject(T7GO).SetGOQuality(ContactQualityValue.NO_SIGNAL);
        PZGOView.SetGameObject(PZGO).SetGOQuality(ContactQualityValue.NO_SIGNAL);
        T8GOView.SetGameObject(T8GO).SetGOQuality(ContactQualityValue.NO_SIGNAL);
        AF4GOView.SetGameObject(AF4GO).SetGOQuality(ContactQualityValue.NO_SIGNAL);
        CMSGOView.SetGameObject(CMSGO).SetGOQuality(ContactQualityValue.NO_SIGNAL);

        return;
      }
      // GameObjects
      AF3GOView.SetGameObject(AF3GO).SetGOQuality(contacts[(int)Channels.AF3]);
      T7GOView.SetGameObject(T7GO).SetGOQuality(contacts[(int)Channels.T7]);
      PZGOView.SetGameObject(PZGO).SetGOQuality(contacts[(int)Channels.O1]);
      T8GOView.SetGameObject(T8GO).SetGOQuality(contacts[(int)Channels.T8]);
      AF4GOView.SetGameObject(AF4GO).SetGOQuality(contacts[(int)Channels.AF4]);
      CMSGOView.SetGameObject(CMSGO).SetGOQuality(contacts[(int)Channels.CMS]);
    }
  }
}