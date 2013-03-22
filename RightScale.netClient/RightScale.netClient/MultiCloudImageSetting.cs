﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A MultiCloudImageSetting defines which settings should be used when a server is launched in a cloud.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeMultiCloudImageSetting.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceMultiCloudImageSettings.html
    /// </summary>
    public class MultiCloudImageSetting:Core.RightScaleObjectBase<MultiCloudImageSetting>
    {

        #region MultiCloudImageSetting.ctor
        /// <summary>
        /// Default Constructor for MultiCloudImageSetting
        /// </summary>
        public MultiCloudImageSetting()
            : base()
        {
        }

        /// <summary>
        /// Constructor for MultiCloudImageSetting object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public MultiCloudImageSetting(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for MultiCloudImageSetting object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public MultiCloudImageSetting(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        #region MultiCloudImageSetting Relationships

        /// <summary>
        /// MultiCloudImage associated with this MultiCloudImageSetting
        /// </summary>
        public MultiCloudImage multiCloudImage
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("multi_cloud_image"));
                return MultiCloudImage.deserialize(jsonString);
            }
        }

        /// <summary>
        /// InstanceType associated with this MultiCloudImageSetting
        /// </summary>
        public InstanceType instanceType
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance_type"));
                return InstanceType.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Image associated with this MultiCloudImageSetting
        /// </summary>
        public Image image
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("image"));
                return Image.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Cloud associated with this MultiCloudImageSetting
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        #endregion

        #region MultiCloudImageSetting.index methods

        /// <summary>
        /// Lists the MultiCloudImageSettings for a MultiCloudImage
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage to query</param>
        /// <returns>List of MultiCloudImageSettings</returns>
        public static List<MultiCloudImageSetting> index(string multiCloudImageID)
        {
            return index(multiCloudImageID, null);
        }

        /// <summary>
        /// Lists the MultiCloudImageSettings for a MultiCloudImage
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage to query</param>
        /// <param name="filter">Set of filters for querying for MultiCloudImageSettings</param>
        /// <returns>List of MultiCloudImageSettings</returns>
        public static List<MultiCloudImageSetting> index(string multiCloudImageID, List<Filter> filter)
        {
            List<string> validFilters = new List<string>() { "cloud_href", "multi_cloud_image_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);
            string getHref = string.Format(APIHrefs.MultiCloudImageSettings, multiCloudImageID);
            string queryString = string.Empty;
            foreach (Filter f in filter)
            {
                queryString += f.ToString() + "&";
            }
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region MultiCloudImageSetting.show methods

        /// <summary>
        /// Shows information about a single MultiCloudImage Setting
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage</param>
        /// <param name="multiCloudImageSettingID">ID of the MultiCloudImage Setting</param>
        /// <returns>Populated instance of MultiCloudImageSetting</returns>
        public static MultiCloudImageSetting show(string multiCloudImageID, string multiCloudImageSettingID)
        {
            string getHref = string.Format(APIHrefs.MultiCloudImageSettingsByID, multiCloudImageID, multiCloudImageSettingID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

        #region MultiCloudImageSetting.create methods

        /// <summary>
        /// Creates a new generic cloud setting for an existing MultiCloudImage
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage</param>
        /// <param name="cloudID">ID of the Cloud</param>
        /// <param name="imageID">ID of the Image</param>
        /// <param name="instanceTypeID">ID of the InstanceType</param>
        /// <returns>ID of newly created MultiCloudImageSetting</returns>
        public static string create(string multiCloudImageID, string cloudID, string imageID, string instanceTypeID)
        {
            return create(multiCloudImageID, cloudID, imageID, instanceTypeID, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a new generic cloud setting for an existing MultiCloudImage
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage</param>
        /// <param name="cloudID">ID of the Cloud</param>
        /// <param name="imageID">ID of the Image</param>
        /// <param name="instanceTypeID">ID of the InstanceType</param>
        /// <param name="kernelImageID">ID of kernel image</param>
        /// <param name="ramdiskImageID">ID of ramdisk image</param>
        /// <param name="userData">User data that RightScale automaticaly passes to your instance at boot time</param>
        /// <returns>ID of newly created MultiCloudImageSetting</returns>
        public static string create(string multiCloudImageID, string cloudID, string imageID, string instanceTypeID, string kernelImageID, string ramdiskImageID, string userData)
        {
            string postHref = string.Format(APIHrefs.MultiCloudImageSettings, multiCloudImageID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(Utility.cloudHref(cloudID), "multi_cloud_image_setting[cloud_href]", postParams);
            Utility.addParameter(Utility.imageHref(cloudID, imageID), "multi_cloud_image_setting[image_href]", postParams);
            Utility.addParameter(Utility.instanceTypeHref(cloudID, instanceTypeID), "multi_cloud_image_setting[instance_type_href]", postParams);
            Utility.addParameter(Utility.kernelImageHref(cloudID, kernelImageID), "multi_cloud_image_setting[kernel_image_href]", postParams);
            Utility.addParameter(Utility.ramdiskImageHref(cloudID, ramdiskImageID), "multi_cloud_image_setting[ramdisk_image_href", postParams);
            Utility.addParameter(userData, "multi_cloud_image_setting[user_data]", postParams);
            string outString;
            return Core.APIClient.Instance.Post(postHref, postParams, "location", out outString).Last<string>().Split('/').Last<string>();
        }

        #endregion

    }
}
