using System;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMUserCustomizeInfo : ZCRMEntity
    {

        private string notesDesc;
        private string isToShowRightPanel;
        private string isBcView;
        private string isToShowHome;
        private string isToShowDetailView;
        private string unpinRecentItem;

        public ZCRMUserCustomizeInfo()
        {
        }

        public static ZCRMUserCustomizeInfo GetInstance()
        {
            return new ZCRMUserCustomizeInfo();
        }

        /// <summary>
        /// Gets or sets the notes desc.
        /// </summary>
        /// <value>The notes desc.</value>
        public string NotesDesc
        {
            get
            {
                return notesDesc;
            }
            set
            {
                notesDesc = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUserCustomizeInfo"/> is to show right panel.
        /// </summary>
        /// <value><c>true</c> if is to show right panel; otherwise, <c>false</c>.</value>
        public string IsToShowRightPanel
        {
            get
            {
                return isToShowRightPanel;
            }
            set
            {
                isToShowRightPanel = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUserCustomizeInfo"/> is bc view.
        /// </summary>
        /// <value><c>true</c> if is bc view; otherwise, <c>false</c>.</value>
        public string IsBcView
        {
            get
            {
                return isBcView;
            }
            set
            {
                isBcView = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUserCustomizeInfo"/> is to show home.
        /// </summary>
        /// <value><c>true</c> if is to show home; otherwise, <c>false</c>.</value>
        public string IsToShowHome
        {
            get
            {
                return isToShowHome;
            }
            set
            {
                isToShowHome = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUserCustomizeInfo"/> is to show detail view.
        /// </summary>
        /// <value><c>true</c> if is to show detail view; otherwise, <c>false</c>.</value>
        public string IsToShowDetailView
        {
            get
            {
                return isToShowDetailView;
            }
            set
            {
                isToShowDetailView = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUserCustomizeInfo"/> unpin recent item.
        /// </summary>
        /// <value><c>true</c> if unpin recent item; otherwise, <c>false</c>.</value>
        public string UnpinRecentItem
        {
            get
            {
                return unpinRecentItem;
            }
            set
            {
                unpinRecentItem = value;
            }
        }
    }
}