using UnityEngine;
using Zenject;
using EmotivUnityPlugin;

namespace dirox.emotiv.controller
{
    public class HeadsetController3D : MonoBehaviour
    {

        [SerializeField] private GameObject insight;
        // we don't use epoc
        // [SerializeField] private GameObject epoc;
        [SerializeField] private float updateCQInterval = 0.5f;

        [Inject]
        public void InjectDependencies (
            ConnectedDevice connectedDevice
        )
        {
            connectedDevice.onHeadsetSelected += setConnectedHeadset;
        }

        private void setConnectedHeadset (Headset selectedHeadsetInformation)
        {
            bool isInsightConnected = Utils.IsInsightType(selectedHeadsetInformation.HeadsetType);

            insight.SetActive(isInsightConnected);
            Debug.Log(insight);
            // epoc.SetActive (!isInsightConnected);
        }

    }
}
