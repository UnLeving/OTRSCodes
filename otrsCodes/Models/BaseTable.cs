namespace otrsCodes.Models
{
    public class BaseTable
    {
        public int R;
        public string AB;
        public CodeDt[] codes = new CodeDt[10];
    }

    public class CodeDt
    {
        public int id;
        public string code;
        public string color = "#FFFFFF";
    }
}