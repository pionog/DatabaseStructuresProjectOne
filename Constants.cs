﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyBazDanych
{
    internal class Constants
    {
        public const int BLOCKS_IN_PAGE = 1024; //number of blocks in a single page
        public const int NUMBERS_IN_RECORD = 5; //number of numbers in a single record
        public const int RECORDS_IN_BLOCK = 16;
        public const int BLOCK_SIZE = NUMBERS_IN_RECORD * sizeof(int) * RECORDS_IN_BLOCK; //size of single block which equals RECORDS_IN_BLOCK records of NUMBERS_IN_RECORD of int
    }
}