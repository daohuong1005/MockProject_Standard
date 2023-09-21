namespace ABSD.Common.Dtos
{
    public class AjaxResult
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
    }
}