﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class MultiCloudImageSettingTest : RSAPITestBase
    {
        public string multiCloudImageID;
        private string multiCloudImageSettingID;

        public MultiCloudImageSettingTest()
        {
            multiCloudImageID = ConfigurationManager.AppSettings["MultiCloudImageSettingTest_multiCloudImageID"].ToString();
            multiCloudImageSettingID = ConfigurationManager.AppSettings["MultiCloudImageSettingTest_multiCloudImageSettingID"].ToString();
        }

        #region MultiCloudImageSetting.index() tests

        [TestMethod]
        public void indexMultiCloudImageSettingSimple()
        {
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count > 0);
        }

        [TestMethod]
        public void indexMultiCloudImageSettingFiltered()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("cloud_href", FilterOperator.NotEqual, "/api/clouds/2432"));
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID, filters);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count>0);
        }

        #endregion

        #region MultiCloudImageSetting.show() tests

        [TestMethod]
        public void showMultiCloudImageSetting()
        {
            MultiCloudImageSetting mcis = MultiCloudImageSetting.show(multiCloudImageID, multiCloudImageSettingID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.links.Count > 0);
        }

        #endregion

        #region MultiCloudImageSetting Relationships tests

        [TestMethod]
        public void multiCloudImageSettingInstanceType()
        {
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count > 0);
            InstanceType instanceType = mcis[0].instanceType;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void multiCloudImageSettingMultiCloudImage()
        {
            
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count > 0);
            MultiCloudImage mci = mcis[0].multiCloudImage;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void multiCloudImageSettingImaeg()
        {
            
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count > 0);
            Image image = mcis[0].image;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void multiCloudImageSettingCloud()
        {
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count > 0);
            Cloud instanceType = mcis[0].cloud;
            Assert.IsTrue(true);
            
        }
        #endregion

        #region MultiCloudImageSetting.create() .destroy() tests

        #endregion

        #region MuliCloudImageSetting.create() .update() .destroy() tests

        #endregion
    }
}
