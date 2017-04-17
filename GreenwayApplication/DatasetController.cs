using System;

namespace GreenwayApplication
{
    public class DatasetController
    {
        public DatasetController()
        {
        }

        /// <summary>
        /// Calculate the price difference between using lowest denomination
        /// </summary>
        /// <param name="ds">Dataset object</param>
        /// <param name="conversionFactors">Integer array of each conversion factor for denominations</param>
        /// <param name="itemFactors">Integer array of items for each denomination</param>
        /// <returns>Price difference between highest and lowest as integer</returns>
        public int CalculatePriceDifference(Dataset ds, int[] conversionFactors, int[][] itemFactors)
        {
            //Method to check passed values
            CheckPassedValues(ds, conversionFactors, itemFactors);


            //Set the ConvertedValue for the Dataset "ds" Denomination objects
            ds = ConvertDenominationsToLowest(conversionFactors, ds);

            int max = 0;
            int min = 0;
            //Iterate itemFactors and multiply it by the ConvertedValue of each denomination object
            for (int i = 0; i < itemFactors.Length; i++)
            {
                int price = 0;
                for (int j = 0; j < itemFactors[i].Length; j++)
                {
                    price += itemFactors[i][j] * ds.Denominations[j];

                }

                if (i == 0)
                {
                    max = price;
                }
                else if (i == 1)
                {
                    if (price > max)
                    {
                        min = max;
                        max = price;
                    }
                    else
                    {
                        min = price;
                    }
                }
                else
                {
                    if (price > max)
                    {
                        max = price;
                    }
                    else if (price < min)
                    {
                        min = price;
                    }
                }
            }

            return (max - min);
        }


        /// <summary>
        /// Data validation to make sure passed data is correct. Throws an error if not.
        /// </summary>
        /// <param name="ds">Dataset object that will hold information</param>
        /// <param name="conversionFactors">Integer array of each conversion factor for denominations</param>
        /// <param name="itemFactors">Integer array of items for each denomination</param>
        private void CheckPassedValues(Dataset ds, int[] conversionFactors, int[][] itemFactors)
        {
            //Quick validation to double check the front end validation
            //Denominations check
            if (ds.NumDenominations > 7 || ds.NumDenominations < 2)
            {
                throw new Exception("The number of denominations should be between 2 and 7, inclusive.");
            }

            //Number of prices check
            if (ds.NumPrices > 10 || ds.NumPrices < 2)
            {
                throw new Exception("The number of prices should be between 2 and 10, inclusive.");
            }

            //Number of conversion factors should be numDenominations - 1
            if (conversionFactors.Length != ds.NumDenominations - 1)
            {
                throw new Exception("The number of conversions should be: " + (ds.NumDenominations - 1));
            }
        }


        /// <summary>
        /// Converts the denominations for the dataset to the lowest valued denomination
        /// </summary>
        /// <param name="values">Denomination conversions</param>
        /// <param name="ds">Dataset to convert</param>
        /// <returns>The dataset with the converted values</returns>
        private Dataset ConvertDenominationsToLowest(int[] values, Dataset ds)
        {
            int[] dens = new int[ds.NumDenominations];
            //go through and convert denomination values to the lowest one
            for (int i = 0; i < ds.NumDenominations; i++)
            {
                if (i == ds.NumDenominations - 1)
                {
                    dens[i] = 1;
                    continue;
                }

                dens[i] = 1;
                for (int j = values.Length - 1; j >= i; j--)
                {
                    dens[i] *= values[j];
                }
            }

            ds.Denominations = dens;
            return ds;
        }
    }
}