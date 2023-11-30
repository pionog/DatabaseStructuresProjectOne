using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    internal class Constants
    {
        //public const int BLOCKS_IN_PAGE = 1024; //number of blocks in a single page
        public const int NUMBERS_IN_RECORD = 5; //number of numbers in a single record
        public const int RECORDS_IN_BUFFER = 6;
        //public const int BUFFER_SIZE = NUMBERS_IN_RECORD * sizeof(double) * RECORDS_IN_BUFFER; //size of single block which equals RECORDS_IN_BLOCK records of NUMBERS_IN_RECORD of int
        public const string NULL_STRING = "?null";
        public const string BACK_STRING = "?back";
        public const double MAX_RANDOM_NUMBER = 100000;
    }
}
