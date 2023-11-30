using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    public class Tape
    {
        public Record[]? buffer {  get; set; }
        public int index {  get; set; }
        public int counter { get; set; }
        public int bufferSize {  get; set; }
        public File file {  get; set; }
        public long offset { get; set; }

        public Tape(File file, bool read)
        {
            this.bufferSize = Constants.RECORDS_IN_BUFFER;
            this.buffer = new Record[bufferSize];
            if (read)
            {
                this.index = bufferSize;
            }
            else
            {
                this.index = 0;
            }
            this.file = file;
            this.counter = 0;
            this.offset = 0;
        }

        public void flushTape() {
            Program.diskSaves++;
            using (var stream = System.IO.File.Open(this.file.path, FileMode.Append))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    for (int j = 0; j < this.index; j++)
                    {
                        Record recordFromBuffer = this.buffer[j]; //taking single record containing NUMBERS_IN_RECORD numbers
                        for (int i = 0; i < Constants.NUMBERS_IN_RECORD; i++)
                        {
                            writer.Write(recordFromBuffer.data[i]);
                        }
                        Console.WriteLine(recordFromBuffer.ToString());
                    }
                    writer.Flush();
                }
            }
        }

        public bool saveRecord(Record record) {
            if (this.index == this.bufferSize) //buffer is full - write it to the disk
            {
                using (var stream = System.IO.File.Open(this.file.path, FileMode.Append))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        for (int j = 0; j < bufferSize; j++)
                        {
                            Record recordFromBuffer = this.buffer[j]; //taking single record containing NUMBERS_IN_RECORD numbers
                            for (int i = 0; i < Constants.NUMBERS_IN_RECORD; i++)
                            {
                                writer.Write(recordFromBuffer.data[i]);
                            }
                            Console.WriteLine(recordFromBuffer.ToString());
                        }
                        writer.Flush();
                    }
                }
                Program.diskSaves++;

                /*using (FileStream fs = System.IO.File.Open(this.file.path, FileMode.Open)) //open file because append cannot allow to do instruction below
                {
                    fs.SetLength(0); //erase content of file by setting its size to zero
                }*/
                for (int i = 0; i < buffer.Length; i++) {
                    buffer[i] = null;
                }
                this.index = 0; //set index to zero because there is new buffer
            }
            if(record == null)
            {
                return false;
            }
            this.buffer[this.index] = record; //save record to buffer
            this.index++; //increase index
            return true;
        }

        public Record? getNextRecord() //get next record from the file. function simulates reading block of records from file and then stores them in a buffer
        {
            bool eof = false; //boolean which checks if there was end of file

            if (this.index == this.bufferSize) //if the buffer is empty then load records into buffer
            {
                Program.diskReads++; //one more disk read
                for (int i = 0; i < this.bufferSize; ++i)
                {
                    this.buffer[i] = null;
                }
                using (var stream = System.IO.File.Open(this.file.path, FileMode.Open))
                {
                    stream.Position = this.offset; //set the stream to the point when it was last time in this file

                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        this.counter = 0;

                        while ((this.counter < this.bufferSize) && !eof)
                        {
                            Record recordFromFile = new(new double[Constants.NUMBERS_IN_RECORD]);
                            for (int i = 0; i < Constants.NUMBERS_IN_RECORD; ++i)
                            {
                                try //it prevents from reading too far
                                {
                                    recordFromFile.data[i] = reader.ReadDouble();
                                }
                                catch //if binaryReader could not read data, then it was the end of file
                                { 
                                    eof = true;
                                    break;
                                }
                            }
                            if (!eof)
                            {
                                this.buffer[this.counter] = recordFromFile;
                                this.counter++;
                            }
                        }
                        this.offset = stream.Position;
                    }
                    
                }
                
                this.index = 0;
                Program.diskReads++;
            }
            if (eof && this.index >= this.counter) 
            {
                return null;
            }
            this.index++;
            return this.buffer[this.index-1];
        }

    }
}
