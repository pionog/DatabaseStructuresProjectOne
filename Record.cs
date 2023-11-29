using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    public class Record
    {
        public double[] data { get; set; } = new double[Constants.NUMBERS_IN_RECORD];

        public Record() //if data was not provided then it should be generated 
        {
            var rand = new Random();
            for (int i = 0; i < this.data.Length; i++)
            {
                this.data[i] = rand.NextDouble() * Constants.MAX_RANDOM_NUMBER;
            }
        }

        public Record(double[] arguments) //data was provided from the input (file or user)
        {
            data = arguments;
        }
        public double geometricMean() //calculate geometric mean from the numbers in the record
        {
            int power = this.data.Length;
            double mean = 1;
            for (int i = 0; i < power; i++) { 
                mean *= this.data[i];
            }
            return Math.Pow(mean, (1.0 / power));
        }
        public override string ToString() //make representation of record in form of string
        {
            string result = "Rekord: {";
            for(int i = 0; i < data.Length-1; i++)
            {
                result += data[i].ToString("F", CultureInfo.CreateSpecificCulture("en-US")); //converting double to string in American format e.g. 12.34
                result += ", "; //comma between numbers
            }
            result += data[data.Length-1].ToString("F", CultureInfo.CreateSpecificCulture("en-US")); //last number
            result += "}"; //after last number there is no comma but bracket
            result += " Średnia geometryczna: ";
            double geo = this.geometricMean();
            result += geo.ToString("F", CultureInfo.CreateSpecificCulture("en-US"));
            return result;
        }
    }
}
