// using UnityEngine;
// using Zenject;
// using EmotivUnityPlugin;

// namespace dirox.emotiv.controller
// {
//     public class Headset3DController : MonoBehaviour
//     {

//         [SerializeField] private GameObject insight;
//         // we don't use epoc
//         // [SerializeField] private GameObject epoc;
//         [SerializeField] private float updateCQInterval = 0.5f;

//         [Inject]
//         public void InjectDependencies (
//             ConnectedDevice connectedDevice
//         )
//         {
//             connectedDevice.onHeadsetSelected += setConnectedHeadset;
//         }

//         private void setConnectedHeadset (Headset selectedHeadsetInformation)
//         {
//             bool isInsightConnected = Utils.IsInsightType(selectedHeadsetInformation.HeadsetType);

//             insight.SetActive (isInsightConnected);
//             // epoc.SetActive (!isInsightConnected);
//             StartCoroutine(RunCoroutineDisplayColor(updateCQInterval));
//         }

//         public void DisplayContactQualityColor() {
//             activeDevice.SetContactQualityColor(DataProcessing.Instance.GetContactQuality());
//         }

//         IEnumerator RunCoroutineDisplayColor(float timeInteval) {
//             while(this.IsActive) {
//                 DisplayContactQualityColor();
//                 yield return new WaitForSeconds(timeInteval);
//             }
//         }
//     }
// }
