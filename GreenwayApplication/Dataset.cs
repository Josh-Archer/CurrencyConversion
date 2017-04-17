namespace GreenwayApplication
{
    public class Dataset
    {
        public int NumDenominations { get; set; }
        public int NumPrices { get; set; }
        public int[] Denominations { get; set; }
        public int PriceDifference { get; set; }

        public Dataset()
        {

        }

        public Dataset(int numDenominations, int numPrices)
        {
            NumDenominations = numDenominations;
            NumPrices = numPrices;
        }
    }
}