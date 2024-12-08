namespace SalesProjection.Application.Abstraction.Projection.Response
{
    public class ProjectionResponse
    {
        public ProjectionResponse(string branch, string region, string period, double salesProjectionValue,
            string strategy) 
        { 
            Branch = branch;
            Region = region;
            Period = period;
            SalesProjectionValue = Math.Round(salesProjectionValue,2);
            Strategy = strategy;
        }
        public string Branch { get; set; }
        public string Region { get; set; }
        public string Period { get; set; }
        public double SalesProjectionValue { get; set; }
        public string Strategy { get; set; }

    }
}
