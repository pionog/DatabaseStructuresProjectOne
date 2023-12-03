using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    public class File //class to operate files easier
    {
        public string path { get; set; } //path to file with its name e.g. "C:\\Program Files\project\file.txt"

        public File(string path) { 
            this.path = path;
        }

        public File makeCopy(string distinguishName = "copy") {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(this.path);
                string directory = Path.GetDirectoryName(this.path);
                string extension = Path.GetExtension(this.path);
                string backupFilePath = Path.Combine(directory, fileName + "-" + distinguishName + extension);
                System.IO.File.Copy(this.path, backupFilePath, true);
                return new(backupFilePath);
            }
            catch {
                throw new Exception("Podana ścieżka jest nieprawidłowa. Podaj poprawną ścieżkę");
            }
        }
        /*
         * 0 - get filename without its extension 1 - get directory name of file 2 - get file's extension
         * Example for: "C:\path\file.ext"
         * 0 - "C:\path\file", 1 - "C:\path", 2 - ".ext"
         */
        public string getSpecificName(int i) {
            switch (i) {
                case 0:
                    return Path.GetFileNameWithoutExtension(this.path);
                case 1:
                    return Path.GetDirectoryName(this.path);
                case 2:
                    return Path.GetExtension(this.path);
                default:
                    return "";
            }
        }
        public void delete() {
            System.IO.File.Delete(this.path);
        }
        public void print()
        {
            bool eof = false;
            using (var stream = System.IO.File.Open(this.path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    while (!eof)
                    {
                        Record record = new();
                        try
                        {
                            for (int i = 0; i < record.data.Length; i++) {
                                record.data[i] = reader.ReadDouble();
                            }
                            Console.WriteLine("\t- " + record.ToString());
                        }
                        catch {
                            eof = true;
                        }
                    }
                }
            }
        }
    }
}
