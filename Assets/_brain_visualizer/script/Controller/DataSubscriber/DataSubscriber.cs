﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotivUnityPlugin;
using Zenject;


namespace dirox.emotiv.controller
{

  /// <summary>
  /// Responsible for subscribing and displaying data
  /// we support for eeg, performance metrics, motion data at this version.
  /// </summary>
  public class DataSubscriber : BaseCanvasView
  {
    DataStreamManager _dataStreamMgr = DataStreamManager.Instance;

    [SerializeField] private Text eegHeader;     // header of eeg data exclude MARKERS
    [SerializeField] private Text eegData;      // eeg data stream
    [SerializeField] private Text motHeader;    // header of motion data
    [SerializeField] private Text motData;      // motion data
    [SerializeField] private Text pmHeader;     // header of performance metric data
    [SerializeField] private Text pmData;       // performance metric data
    float _timerDataUpdate = 0;
    const float TIME_UPDATE_DATA = .01f;

    [SerializeField] private GameObject motionCube;       // a simple cube we can manipulate with data

    // Quaternion values
    private double qW;
    private double qX;
    private double qY;
    private double qZ;

    // Acceleration values
    private double accelerationX;
    private double accelerationY;
    private double accelerationZ;

    // create an object that contains channel data and label
    /*    public class QuaternionValue
        {
          // Auto-implemented properties.
          public string? Channel { get; set; }
          public int Data { get; set; }
        }

        // create a list of objects that contains the channel objects
        class QuaternionValues
        {
          static values = new List<QuaternionValue>();

          public bool AddQuaternionValue(string channel, int data)
          {
            values.Add(new QuaternionValue { Channel = channel, Data = data });
            return true;
          }
        }*/

    void Update()
    {
      if (!this.isActive) {
        return;
      }

      _timerDataUpdate += Time.deltaTime;
      if (_timerDataUpdate < TIME_UPDATE_DATA)
        return;

      _timerDataUpdate -= TIME_UPDATE_DATA;

      // update EEG data
      if (DataStreamManager.Instance.GetNumberEEGSamples() > 0) {
        string eegHeaderStr = "EEG Header: ";
        string eegDataStr = "EEG Data: ";
        foreach (var ele in DataStreamManager.Instance.GetEEGChannels()) {
          string chanStr = ChannelStringList.ChannelToString(ele);
          double[] data = DataStreamManager.Instance.GetEEGData(ele);
          eegHeaderStr += chanStr + ", ";
          if (data != null && data.Length > 0)
            eegDataStr += data[0].ToString() + ", ";
          else
            eegDataStr += "null, "; // for null value
        }
        eegHeader.text = eegHeaderStr;
        eegData.text = eegDataStr;
      }

      // update motion data
      if (DataStreamManager.Instance.GetNumberMotionSamples() > 0) {
        string motHeaderStr = "Motion Header: ";
        string motDataStr = "Motion Data: ";

        // create an array of the channels we want to use
        /*string[] quaternianChannels = new string[] { "CHAN_Q0", "CHAN_Q1", "CHAN_Q2", "CHAN_Q3" };*/

        // initalize an empty list
        /*var _qValues = new QuaternionValues();*/

                // get each element from within the data stream manager
                foreach (var ele in DataStreamManager.Instance.GetMotionChannels())
                {
                  string chanStr = ChannelStringList.ChannelToString(ele);
                  // double is similar to a float
                  double[] data = DataStreamManager.Instance.GetMotionData(ele);

                  motHeaderStr += chanStr + ", ";
          Debug.Log(chanStr);
          if (data != null && data.Length > 0)
          {
            motDataStr += data[0].ToString() + ", ";

            if (chanStr == "Q0") qW = data[0];
            if (chanStr == "Q1") qW = data[0];
            if (chanStr == "Q2") qX = data[0];
            if (chanStr == "Q3") qY = data[0];
            if (chanStr == "Q0") qZ = data[0];
            if (chanStr == "ACCX") accelerationX = data[0];
            if (chanStr == "ACCY") accelerationY = data[0];
            if (chanStr == "ACCZ") accelerationZ = data[0];
          }


          else
            motDataStr += "null, "; // for null value
          }
            motHeader.text = motHeaderStr;
            motData.text = motDataStr;

            // Update the rotation based on quaternion values
            Quaternion rotation = new Quaternion((float)qX, (float)qY, (float)qZ, (float)qW);
            motionCube.transform.rotation = rotation;

            // Apply rotation based on acceleration values
            Vector3 acceleration = new Vector3((float)accelerationX, (float)accelerationY, (float)accelerationZ);
            motionCube.transform.Rotate(acceleration * Time.deltaTime);
      }
      // update pm data
      if (DataStreamManager.Instance.GetNumberPMSamples() > 0) {
                string pmHeaderStr = "Performance metrics Header: ";
                string pmDataStr   = "Performance metrics Data: ";
                bool hasPMUpdate = true;
                foreach (var ele in DataStreamManager.Instance.GetPMLists()) {
                    string chanStr  = ele;
                    double data     = DataStreamManager.Instance.GetPMData(ele);
                    Debug.Log(data);
                    if (chanStr == "TIMESTAMP" && (data == -1))
                    {
                        // has no new update of performance metric data
                        hasPMUpdate = false;
                        break;
                    }
                    pmHeaderStr    += chanStr + ", ";
                    pmDataStr      +=  data.ToString() + ", ";
                }
                if (hasPMUpdate) {
                    pmHeader.text  = pmHeaderStr;
                    pmData.text    = pmDataStr;
                }

            }
        }

        public override void Activate()
        {
            Debug.Log("DataSubscriber: Activate");
            base.Activate();
        }

        public override void Deactivate()
        {
            Debug.Log("DataSubscriber: Deactivate");
            base.Deactivate();
        }

        /// <summary>
        /// Subscribe EEG data
        /// </summary>
        public void onEEGSubBtnClick() {
            Debug.Log("onEEGSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.EEG};
            _dataStreamMgr.SubscribeMoreData(dataStreamList);
        }

        /// <summary>
        /// UnSubscribe EEG data
        /// </summary>
        public void onEEGUnSubBtnClick() {
            Debug.Log("onEEGUnSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.EEG};
            _dataStreamMgr.UnSubscribeData(dataStreamList);
            // clear text
            eegHeader.text  = "EEG Header: ";
            eegData.text    = "EEG Data: ";
        }

        /// <summary>
        /// Subscribe Motion data
        /// </summary>
        public void onMotionSubBtnClick() {
            Debug.Log("onMotionSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.Motion};
            _dataStreamMgr.SubscribeMoreData(dataStreamList);
        }

        /// <summary>
        /// UnSubscribe Motion data
        /// </summary>
        public void onMotionUnSubBtnClick() {
            Debug.Log("onMotionUnSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.Motion};
            _dataStreamMgr.UnSubscribeData(dataStreamList);
            // clear text
            motHeader.text  = "Motion Header: ";
            motData.text    = "Motion Data: ";
        }

        /// <summary>
        /// Subscribe Performance metrics data
        /// </summary>
        public void onPMSubBtnClick() {
            Debug.Log("onPMSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.PerformanceMetrics};
            _dataStreamMgr.SubscribeMoreData(dataStreamList);
        }

        /// <summary>
        /// UnSubscribe Performance metrics data
        /// </summary>
        public void onPMUnSubBtnClick() {
            Debug.Log("onPMUnSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.PerformanceMetrics};
            _dataStreamMgr.UnSubscribeData(dataStreamList);
            // clear text
            pmHeader.text  = "Performance metrics Header: ";
            pmData.text    = "Performance metrics Data: ";
        }
    }
}

