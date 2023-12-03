using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    internal class Constants
    {
        public const int NUMBERS_IN_RECORD = 5; //number of numbers in a single record
        public const int RECORDS_IN_BUFFER = 20;
        public const string NULL_STRING = "?null";
        public const string BACK_STRING = "?back";
        public const string LIST_ELEMENT = "\t- ";
        public const double MAX_RANDOM_NUMBER = 100000; //set maximum number which is available to achive by rng
        public const int FILES_IN_DIRECTORY = 100; //set how many files there are to test sorting
    }
}
