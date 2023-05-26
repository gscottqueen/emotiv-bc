using UnityEngine;
using UnityEngine.UI;
using Zenject;
using EmotivUnityPlugin;

namespace dirox.emotiv.controller
{
    public class ContactQualityInsightManager : ContactQualityBaseManager
    {
        [SerializeField] Image AF3, T7, PZ, T8, AF4, CMS;
        [SerializeField] GameObject AF3GO;

        // images
        /*[Inject (Id = "AF3")] ContactQualityNodeView AF3View;*/
        [Inject (Id = "T7")]  ContactQualityNodeView T7View;
        [Inject (Id = "PZ")]  ContactQualityNodeView PZView;
        [Inject (Id = "T8")]  ContactQualityNodeView T8View;
        [Inject (Id = "AF4")] ContactQualityNodeView AF4View;
        [Inject (Id = "CMS")] ContactQualityNodeView CMSView;

        // GameObjects
        [Inject(Id = "AF3")] ContactQualityNodeView AF3GOView;

    protected override void SetColorSettingForNodes (Color[] allColors)
        {
            // images
            /*AF3View.SetColors (allColors).SetDisplay (AF3);*/
            T7View.SetColors(allColors).SetDisplay(T7);
            PZView.SetColors(allColors).SetDisplay(PZ);
            T8View.SetColors(allColors).SetDisplay(T8);
            AF4View.SetColors(allColors).SetDisplay(AF4);
            CMSView.SetColors(allColors).SetDisplay(CMS);

            // GameObjects
            AF3GOView.SetGameObjectColors(allColors).SetGameObject(AF3GO);
        }

    public override void SetContactQualityColor(ContactQualityValue[] contacts) {
        if (contacts == null) {
            // images
            /*AF3View.SetDisplay(AF3).SetQuality(ContactQualityValue.NO_SIGNAL);*/
            T7View.SetDisplay(T7).SetQuality(ContactQualityValue.NO_SIGNAL);
            PZView.SetDisplay(PZ).SetQuality(ContactQualityValue.NO_SIGNAL);
            T8View.SetDisplay(T8).SetQuality(ContactQualityValue.NO_SIGNAL);
            AF4View.SetDisplay(AF4).SetQuality(ContactQualityValue.NO_SIGNAL);
            CMSView.SetDisplay(CMS).SetQuality(ContactQualityValue.NO_SIGNAL);

            // GameObjects
            AF3GOView.SetGameObject(AF3GO).SetGOQuality(ContactQualityValue.NO_SIGNAL);

            return;
        }

        // images
        /*AF3View.SetDisplay(AF3).SetQuality(contacts[(int)Channels.AF3]);*/
        T7View.SetDisplay(T7).SetQuality(contacts[(int)Channels.T7]);
        PZView.SetDisplay(PZ).SetQuality(contacts[(int)Channels.O1]);
        T8View.SetDisplay(T8).SetQuality(contacts[(int)Channels.T8]);
        AF4View.SetDisplay(AF4).SetQuality(contacts[(int)Channels.AF4]);
        CMSView.SetDisplay(CMS).SetQuality(contacts[(int)Channels.CMS]);

        // GameObjects
        AF3GOView.SetGameObject(AF3GO).SetGOQuality(contacts[(int)Channels.AF3]);
      }
    }
}