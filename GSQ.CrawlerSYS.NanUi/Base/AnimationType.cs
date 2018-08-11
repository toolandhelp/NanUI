namespace GSQ.CrawlerSYS.NanUi.Base
{
    /// <summary>
    /// 动画样式枚举
    /// </summary>
    public enum AnimationType
    {
        /// <summary>
        /// winform默认普通效果显示
        /// </summary>
        Ordinary = 0,

        /// <summary>
        /// 从左向右显示
        /// </summary>
        LeftToRight = 1,

        /// <summary>
        /// 从右向左显示
        /// </summary>
        RightToLeft = 2,

        /// <summary>
        /// 从上到下显示
        /// </summary>
        TopToBottom = 3,

        /// <summary>
        /// 从下到上显示
        /// </summary>
        BottomToTop = 4,

        /// <summary>
        /// 透明渐变显示
        /// </summary>
        Gradient = 5,

        /// <summary>
        /// 从中间向四周
        /// </summary>
        Center = 6,

        /// <summary>
        /// 左上角伸展
        /// </summary>
        Slide_LeftTop = 7,

        /// <summary>
        /// 左下角伸展
        /// </summary>
        Slide_LeftBottom = 8,

        /// <summary>
        /// 右上角伸展
        /// </summary>
        Slide_RightTop = 9,
        /// <summary>
        /// 右下角伸展
        /// </summary>
        Slide_RightBottom = 10
    }
}