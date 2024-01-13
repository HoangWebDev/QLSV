namespace QLSV.Response
{
    public class BaseReponse
    {
        public int ErrCode { get; set; }
        public string ErrMess { get; set; }
    }

    public class AddSinhVienResponse : BaseReponse { }
}
