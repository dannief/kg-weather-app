namespace KG.Weather.Data.Models
{
    public class WorkerType: Enumeration<WorkerType>
    {
        public static readonly WorkerType
            Canner     = new WorkerType("Canner"),
            Packer     = new WorkerType("Packer"),
            Technician = new WorkerType("Technician");

        public WorkerType(): base()
        {
        }

        private WorkerType(string value): base(value)
        {
        }

        public WorkerType Clone()
        {
            return new WorkerType(Value);
        }
    }
}
