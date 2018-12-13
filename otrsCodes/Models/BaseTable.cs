namespace otrsCodes.Models
{
    public class BaseTable
    {
        public int R;
        public int AB;
        public CodeDt[] codes = new CodeDt[10];
    }

    public class CodeDt
    {
        public int id;
        public int code;
        public string color = "#212529";
    }
}