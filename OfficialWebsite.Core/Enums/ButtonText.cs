namespace OfficialWebsite.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// 按鈕文字
    /// </summary>
    public enum ButtonText
    {
        [Description("新增")]
        Create = 0,

        [Description("修改")]
        Update = 1,

        [Description("刪除")]
        Delete = 2,

        [Description("取消")]
        Cancle = 3,

        [Description("查詢")]
        Query = 4,

        [Description("儲存")]
        Save = 6,

        [Description("回上一頁")]
        GoBack = 8,

        [Description("送出")]
        Submit = 16,

        [Description("關閉")]
        Close = 32,

        [Description("儲存修改")]
        Modify = 64,

        [Description("清除")]
        Reset = 128,
    }
}
