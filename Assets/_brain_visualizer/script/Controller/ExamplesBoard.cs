using UnityEngine;
using Zenject;

namespace dirox.emotiv.controller
{
    /// <summary>
    /// Board of examples
    /// </summary>
    public class ExamplesBoard : BaseCanvasView 
    {

        // data subscribers
        DataSubscriber dataSubscriber;
        
        [Inject]
        public void SetDependencies(DataSubscriber subscriber)
        {
           dataSubscriber = subscriber;
        }
        public override void Activate()
        {
          Deactivate();
          dataSubscriber.Activate();
        }
    }
}