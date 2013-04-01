﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Threading;


namespace RightScale.netClient.Test
{
    [TestClass]
    public class VolumeAttachmentTest
    {
        private string cloudID;
        private string apiRefreshToken;
        private string serverID;
        private int maxWaitLoops;
        private string volumeTypeID;

        public VolumeAttachmentTest()
        {
            cloudID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeAttachmentTest_cloudID"].ToString());
            apiRefreshToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            serverID = ConfigurationManager.AppSettings["VolumeAttachmentTest_serverID"].ToString();
            volumeTypeID = ConfigurationManager.AppSettings["VolumeAttachmentTest_volumeTypeID"].ToString();
            maxWaitLoops = 30;
        }

        [TestMethod]
        public void volumeAttachmentIndexSimple()
        {
            List<VolumeAttachment> volAttachments = VolumeAttachment.index(cloudID);
            Assert.IsNotNull(volAttachments);
        }

        
        [TestMethod]
        public void volumeAttachmentIndexComplicated()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            List<VolumeAttachment> volAttachments = VolumeAttachment.index(cloudID);
            netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void volumeAttachFullEndToEnd()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);

            string currentState = Server.show(serverID).state;
            int waitLoops = 0;

            if (currentState != "inactive")
            {
                try
                {
                    Server.terminate(serverID);
                }
                catch
                {

                }
            }

            while (currentState != "inactive" && (waitLoops <= maxWaitLoops || maxWaitLoops < 0))
            {
                Thread.Sleep(5000);
                currentState = Server.show(serverID).state;
                waitLoops++;
            }
            if (waitLoops >= maxWaitLoops && maxWaitLoops >= 0)
            {
                Assert.Fail("Cannot start test because server is not in an available state.  Test has not started");
            }

            bool result = Server.launch(serverID);
            Assert.IsTrue(result);

            currentState = Server.show(serverID).state;

            while (currentState != "operational")
            {
                Thread.Sleep(5000);
                currentState = Server.show(serverID).state;
            }

            string newVolumeID = Volume.create(cloudID, "testVolume", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "100", volumeTypeID);
            
            string volStatus = Volume.show(cloudID, newVolumeID).status;
            while (volStatus != "available")
            {
                Thread.Sleep(5000);
                volStatus = Volume.show(cloudID, newVolumeID).status;
            }

            Volume newVolume = Volume.show(cloudID, newVolumeID);
            //test volume created

            string volAttachID = VolumeAttachment.create(cloudID, "/dev/xvh", Server.show(serverID).currentInstance.ID, newVolumeID);
            string volAttachStatus = VolumeAttachment.show(cloudID, volAttachID).state;

            while (volAttachStatus != "attached")
            {
                Thread.Sleep(5000);
                volStatus = VolumeAttachment.show(cloudID, volAttachID).state;
            }

            VolumeAttachment newVolumeAttachment = VolumeAttachment.show(cloudID, volAttachID);
            //test volume attachment stats

            bool volumeDetachStatus = VolumeAttachment.destroy(cloudID, volAttachID);
            Assert.IsTrue(volumeDetachStatus);
            bool volumeDestroyStatus = Volume.destroy(cloudID, newVolumeID);
            Assert.IsTrue(volumeDestroyStatus);
            bool serverTerminateCall = Server.terminate(serverID);
            Assert.IsTrue(serverTerminateCall);
        }
    }
}
