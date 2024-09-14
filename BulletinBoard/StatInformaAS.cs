namespace 工艺部信息化组
{
    public class StatInformaAS
    {
        /// <summary>
        /// 是否查询MES成功
        /// </summary>
        public bool IsHandle { get; set; }
        /// <summary>
        /// 处理是否成功
        /// </summary>
        public bool IsProcess {  get; set; }

        public string WorkInformats {  get; set; }

        public string ExStr {  get; set; }
        public string ORDER { get; set; }

        public string ITEM { get; set; }

        public string ITEM_DESC { get; set; }

        public string ROUTER { get; set; }

        public string ORDER_NUM { get; set; }

        public string COMP_NUM { get; set; }

        public string COMP_RATE { get; set; }

        public string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", ORDER, ITEM, ITEM_DESC, ROUTER, ORDER_NUM, COMP_NUM, COMP_RATE);
        }

    }
}