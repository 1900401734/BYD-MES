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
        public string DeviceStatusPoint { get; set; }      // 设备状态 

        /// <summary>
        /// 产品型号：D1120
        /// </summary>
        [SugarColumn(ColumnName = "ProductModelNum", IsNullable = true)]
        public string ProductModelPoint { get; set; }       // 产品型号 

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

        [SugarColumn(ColumnName = "StartNFC", IsNullable = true)]
        public string StartNFCPoint { get; set; }

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
