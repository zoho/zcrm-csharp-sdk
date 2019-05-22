using System;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMUserTheme : ZCRMEntity
    {
        private string normalTabFontColor;
        private string normalTabBackground;
        private string selectedTabFontColor;
        private string selectedTabBackground;
        private string new_background;
        private string background;
        private string screen;
        private string type;

        public ZCRMUserTheme()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        public static ZCRMUserTheme GetInstance()
        {
            return new ZCRMUserTheme();
        }

        /// <summary>
        /// Gets or sets the color of the normal tab font.
        /// </summary>
        /// <value>The color of the normal tab font.</value>
        public string NormalTabFontColor
        {
            get
            {
                return normalTabFontColor;
            }
            set
            {
                normalTabFontColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the normal tab background.
        /// </summary>
        /// <value>The normal tab background.</value>
        public string NormalTabBackground
        {
            get
            {
                return normalTabBackground;
            }
            set
            {
                normalTabBackground = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selected tab font.
        /// </summary>
        /// <value>The color of the selected tab font.</value>
        public string SelectedTabFontColor
        {
            get
            {
                return selectedTabFontColor;
            }
            set
            {
                selectedTabFontColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected tab background.
        /// </summary>
        /// <value>The selected tab background.</value>
        public string SelectedTabBackground
        {
            get
            {
                return selectedTabBackground;
            }
            set
            {
                selectedTabBackground = value;
            }
        }

        /// <summary>
        /// Gets or sets the new background.
        /// </summary>
        /// <value>The new background.</value>
        public string New_background
        {
            get
            {
                return new_background;
            }
            set
            {
                new_background = value;
            }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public string Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
            }
        }

        /// <summary>
        /// Gets or sets the screen.
        /// </summary>
        /// <value>The screen.</value>
        public string Screen
        {
            get
            {
                return screen;
            }
            set
            {
                screen = value;
            }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
    }
}