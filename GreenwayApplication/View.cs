using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenwayApplication
{
    /**
     * View wil handle some validation and data extraction
     **/
    public class View
    {
        public static void Main(string[] args)
        {
            try
            {
                //Enter file location
                string line;

                // Read the file and display it line by line.
                Console.Write("File location: ");
                System.IO.StreamReader file = new System.IO.StreamReader(Console.ReadLine());
                if ((line = file.ReadLine()) != null)
                {
                    Dataset[] dataSets = new Dataset[GetNumberOfDataSets(line.Split())];

                    for (int i = 1; i <= dataSets.Length; i++)
                    {
                        //get line
                        string[] words = file.ReadLine().Split();

                        int numDenominations = GetNumberOfDenominations(words);
                        int numPrices = GetNumberOfPrices(words);

                        Dataset ds = new Dataset(numDenominations, numPrices);

                        //get next line
                        words = file.ReadLine().Split();

                        DatasetController controller = new DatasetController();

                        //Have GetConversionFactors and GetItemFactors so that View remains seperate from logic. 
                        int priceDiff = controller.CalculatePriceDifference(ds, GetConversionFactors(words, numDenominations), GetItemFactors(file, numPrices, numDenominations));

                        Console.WriteLine("Data Set: " + i);
                        Console.WriteLine(priceDiff);
                    }
                    file.Close();
                    Console.ReadLine();
                }                
             
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        private static int[][] GetItemFactors(System.IO.StreamReader file, int numPrices, int numDenominations)
        {
            //Array to store the Items
            int[][] items = new int[numPrices][];

            //Go to each price
            for (int j = 0; j < numPrices; j++)
            {
                string[] line = file.ReadLine().Split();

                //Check that the right amount is inputed
                if (line.Length != numDenominations)
                {
                    throw new Exception("A line that contains the number of items is malformed.");
                }

                items[j] = new int[numDenominations];

                //Iterate through the denominations
                for (int k = 0; k < line.Length; k++)
                {
                    int parsedNum;
                    //try parse
                    if (!Int32.TryParse(line[k], out parsedNum))
                    {
                        throw new Exception("A line that contains the number of each item is malformed.");
                    }

                    //Input parsed number
                    items[j][k] = parsedNum;
                }
            }

            return items;
        }

        private static int[] GetConversionFactors(string[] stringValues, int numDenominations)
        {
            //convert the values into int[] to be passed
            int[] conversionValues = new int[stringValues.Length];
            for (int j = 0; j < stringValues.Length; j++)
            {
                //Check parse
                if (!Int32.TryParse(stringValues[j], out conversionValues[j]))
                {
                    throw new Exception ("The third line of the data set, second entry, must contain integers");
                }

                //check if negative
                if (conversionValues[j] < 0)
                {
                    throw new Exception ("The third line of the data set, second entry, must contain integer greater than 0");
                }
            }

            return conversionValues;
        }

        private static int GetNumberOfPrices(string[] line)
        {
            int numPrices;

            //Try conversion of numPrices
            bool parseBool = Int32.TryParse(line[1].ToString(), out numPrices);


            //Check if conversion worked and the number of prices falls in the correct area
            if (!parseBool || (numPrices > 10 || numPrices < 2))
            {
                throw new Exception ("The second line of the data set, second entry, must contain an integer from 2-10");
            }

            return numPrices;
        }

        private static int GetNumberOfDenominations(string[] line)
        {
            int numDenominations;
            //Check line length
            if (line.Length != 2)
            {
                throw new Exception ("The first line of a data set is malformed. Should have 2 numbers.");
            }

            //Get next line for # of denominations
            // line = Console.ReadLine().Split();


            //Try conversion 
            bool parseBool = Int32.TryParse(line[0], out numDenominations);

            //Parse failed or the number of denominations does not fall in range
            if (!parseBool || (numDenominations > 7 || numDenominations < 2))
            {
                throw new Exception ("The second line of the data set, first entry, must contain an integer from 2-7");
            }

            return numDenominations;
        }

        private static int GetNumberOfDataSets(string[] line)
        {
            int dataSets;
            bool parse = Int32.TryParse(line[0], out dataSets);

            //Parse failed so not a number
            if (!parse)
            {
                throw new Exception ("The first line must contain an integer.");
            }

            if (dataSets <= 0)
            {
                throw new Exception ("The first line must contain a number greater than 0.");
            }

            return dataSets;
        }
    }
}
