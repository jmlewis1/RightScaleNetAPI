﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class InstanceTypeTest
    {
        string cloudID;

        public InstanceTypeTest()
        {
            cloudID = ConfigurationManager.AppSettings["InstanceTypeTest_cloudID"].ToString();
        }

        #region InstanceType.index tests

        [TestMethod]
        public void InstanceTypeIndexTest()
        {
            List<InstanceType> resultSet = InstanceType.index(cloudID);
            Assert.IsNotNull(resultSet);
            Assert.IsTrue(resultSet.Count > 0);
        }

        [TestMethod]
        public void InstanceTypeIndexDefaultTest()
        {
            List<InstanceType> resultSet = InstanceType.index(cloudID, "default");
            Assert.IsNotNull(resultSet);
            Assert.IsTrue(resultSet.Count > 0);
        }

        [TestMethod]
        public void InstanceTypeIndexFilteredTest()
        {
            List<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
            filters.Add(new KeyValuePair<string, string>("name", "extra"));
            List<InstanceType> filteredResultSet = InstanceType.index(cloudID, filters);
            Assert.IsNotNull(filteredResultSet);
            Assert.IsTrue(filteredResultSet.Count > 0);

            List<InstanceType> resultSet = InstanceType.index(cloudID);
            Assert.IsNotNull(resultSet);
            Assert.IsTrue(resultSet.Count > 0);

            Assert.IsTrue(filteredResultSet.Count <= resultSet.Count);
        }

        [TestMethod]
        public void InstanceTypeIndexFullTest()
        {
            
            List<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
            filters.Add(new KeyValuePair<string, string>("name", "extra"));
            List<InstanceType> filteredResultSet = InstanceType.index(cloudID, filters, "default");
            Assert.IsNotNull(filteredResultSet);
            Assert.IsTrue(filteredResultSet.Count > 0);

            List<InstanceType> resultSet = InstanceType.index(cloudID);
            Assert.IsNotNull(resultSet);
            Assert.IsTrue(resultSet.Count > 0);

            Assert.IsTrue(filteredResultSet.Count <= resultSet.Count);
        }

        #endregion
    }
}
