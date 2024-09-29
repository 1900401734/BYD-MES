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

        [SugarColumn(ColumnName = "DeviceStatus", IsNullable = true)]
        public string DeviceStatus { get; set; }      // 设备状态 

        [SugarColumn(ColumnName = "ProductModelNum", IsNullable = true)]
        public string ProductModel { get; set; }       // 产品型号 

        [SugarColumn(ColumnName = "ProductModelLength", IsNullable = true)]
        public string ProductModelLength { get; set; }    // 型号长度 

        [SugarColumn(ColumnName = "FormulaNum", IsNullable = true)]
        public string RecipeIdPoint { get; set; }    // 配方号 

        [SugarColumn(ColumnName = "FormulaModify", IsNullable = true)]
        public string FormulaModify { get; set; }     // 配方修改 

        [SugarColumn(ColumnName = "FormulaNumModify", IsNullable = true)]
        public string FormulaNumModify { get; set; }      // 配方号修改 

        [SugarColumn(ColumnName = "StartNFC", IsNullable = true)]
        public string StartNFC { get; set; } // 开始NFC

        [SugarColumn(ColumnName = "EndNFC", IsNullable = true)]
        public string EndNFC { get; set; } // 结束NFC 默认：D18040

        [SugarColumn(ColumnName = "ViewStatus", IsNullable = true)]
        public string ViewStatus { get; set; } // 开始NFC修改

        public static DeviceInformation DeviceInformationInitalize()
        {
            DeviceInformation deviceInformation = new DeviceInformation();
            deviceInformation.ID = 1;
            deviceInformation.LanguageId = 0;
            deviceInformation.DeviceStatus = "D1007";       // 设备状态
            deviceInformation.ProductModel = "D1120";
            deviceInformation.ProductModelLength = "10";
            deviceInformation.RecipeIdPoint = "D1208";
            deviceInformation.FormulaModify = "D1204";
            deviceInformation.FormulaNumModify = "D1206";

            return deviceInformation;
        }

        public string Save()
        {
            if (string.IsNullOrWhiteSpace(DeviceStatus))
            {
                DeviceStatus = "D1007";
            }
            if (string.IsNullOrWhiteSpace(ProductModel))
            {
                ProductModel = "D1120";
            }
            if (string.IsNullOrWhiteSpace(ProductModelLength))
            {
                ProductModelLength = "10";
            }
            if (string.IsNullOrWhiteSpace(RecipeIdPoint))
            {
                RecipeIdPoint = "D1208";
            }
            if (string.IsNullOrWhiteSpace(FormulaModify))
            {
                FormulaModify = "D1204";
            }
            if (string.IsNullOrWhiteSpace(FormulaNumModify))
            {
                FormulaNumModify = "D1206";
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
