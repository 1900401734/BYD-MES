using MesDatas.DatasServer;
using SqlSugar;
namespace MesDatas.DatasModel
{
    [SugarTable("DeviceInformation")]

    public class DeviceInformation
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }      // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        public int LanguageId { get; set; }      // 语言ID 

        /// <summary>
        /// 设备状态：D1007
        /// </summary>
        [SugarColumn(ColumnName = "DeviceStatus", IsNullable = true)]
        public string DeviceStatusPoint { get; set; }

        /// <summary>
        /// 产品型号：D1120
        /// </summary>
        [SugarColumn(ColumnName = "ProductModelNum", IsNullable = true)]
        public string ProductModelPoint { get; set; }

        /// <summary>
        /// 型号长度：10
        /// </summary>
        [SugarColumn(ColumnName = "ProductModelLength", IsNullable = true)]
        public string ProductModelLength { get; set; }

        /// <summary>
        /// 配方号：D1208
        /// </summary>
        [SugarColumn(ColumnName = "FormulaNum", IsNullable = true)]
        public string RecipeIdPoint { get; set; }

        /// <summary>
        /// 配方修改：D1204
        /// </summary>
        [SugarColumn(ColumnName = "FormulaModify", IsNullable = true)]
        public string ModifyRecipePoint { get; set; }

        /// <summary>
        /// 配方号修改：D1206
        /// </summary>
        [SugarColumn(ColumnName = "FormulaNumModify", IsNullable = true)]
        public string ModifyRecipeIDPoint { get; set; }

        /// <summary>
        /// 刷卡返回：D18040
        /// </summary>
        [SugarColumn(ColumnName = "EndNFC", IsNullable = true)]
        public string EndNFCPoint { get; set; }

        /// <summary>
        /// 看板连接状态
        /// </summary>
        [SugarColumn(ColumnName = "ViewStatus", IsNullable = true)]
        public string DashboardStatusPoint { get; set; }

        /// <summary>
        /// 英文机台名称
        /// </summary>
        [SugarColumn(ColumnName = "DeviceName_English", IsNullable = true)]
        public string DeviceName_English { get; set; } = "DeviceName_English";

        /// <summary>
        /// 泰文机台名称
        /// </summary>
        [SugarColumn(ColumnName = "DeviceName_Thai", IsNullable = true)]
        public string DeviceName_Thai { get; set; } = "ชื่ออุปกรณ์_ไทย";

        /// <summary>
        /// 工位名称（英文）
        /// </summary>
        [SugarColumn(ColumnName = "StationNameSets_English", IsNullable = true)]
        public string StationNameSets_English { get; set; } = "Left|Right";

        /// <summary>
        /// 工位名称（泰文）
        /// </summary>
        [SugarColumn(ColumnName = "StationNameSets_Thai", IsNullable = true)]
        public string StationNameSets_Thai { get; set; } = "Left|Right";

        public static DeviceInformation DeviceInformationInitalize()
        {
            DeviceInformation deviceInformation = new DeviceInformation();
            deviceInformation.ID = 1;
            deviceInformation.LanguageId = 0;
            deviceInformation.DeviceStatusPoint = "D1007";       // 设备状态
            deviceInformation.ProductModelPoint = "D1120";
            deviceInformation.ProductModelLength = "10";
            deviceInformation.RecipeIdPoint = "D1208";
            deviceInformation.ModifyRecipePoint = "D1204";
            deviceInformation.ModifyRecipeIDPoint = "D1206";

            return deviceInformation;
        }

        public string Save()
        {
            if (string.IsNullOrWhiteSpace(DeviceStatusPoint))
            {
                DeviceStatusPoint = "D1007";
            }
            if (string.IsNullOrWhiteSpace(ProductModelPoint))
            {
                ProductModelPoint = "D1120";
            }
            if (string.IsNullOrWhiteSpace(ProductModelLength))
            {
                ProductModelLength = "10";
            }
            if (string.IsNullOrWhiteSpace(RecipeIdPoint))
            {
                RecipeIdPoint = "D1208";
            }
            if (string.IsNullOrWhiteSpace(ModifyRecipePoint))
            {
                ModifyRecipePoint = "D1204";
            }
            if (string.IsNullOrWhiteSpace(ModifyRecipeIDPoint))
            {
                ModifyRecipeIDPoint = "D1206";
            }

            return DeviceInformationServer.GetDeviceInformationSave(this);
        }
        public string Update()
        {
            return DeviceInformationServer.GetDeviceInformationUpdate(this);
        }
        public string Delete()
        {
            return DeviceInformationServer.GetDeviceInformationDelete(this);
        }
    }
}
