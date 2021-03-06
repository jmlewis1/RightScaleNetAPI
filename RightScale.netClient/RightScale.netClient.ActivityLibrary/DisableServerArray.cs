﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary
{
    /// <summary>
    /// Disable ServerArray custom CodeActivity disables a given ServerArray by ID
    /// </summary>
    public sealed class DisableServerArray : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of ServerArray to disable
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }

        /// <summary>
        /// Output variable indicating that the ServerArray was disabled
        /// </summary>
        public OutArgument<bool> isDisabled { get; set; }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
            LogInformation("Beginning call to disable ServerArray " + this.serverArrayID.Get(context));
            
            if (base.authClient(context))
            {
                bool setDisabled = ServerArray.setDisabled(this.serverArrayID.Get(context));
                isDisabled.Set(context, setDisabled);
                retVal = true;
            }

            LogInformation("Completed call to disable ServerArray " + this.serverArrayID.Get(context) + " with return value of " + this.isDisabled.Get(context).ToString());
            return retVal;
        }
        
        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Disable ServerArray";
        }
    }
}
