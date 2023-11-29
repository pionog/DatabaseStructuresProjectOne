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

        public File makeBackup() {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(this.path);
                string directory = Path.GetDirectoryName(this.path);
                string extension = Path.GetExtension(this.path);
                string backupFilePath = Path.Combine(directory, fileName + "-backup" + extension);
                System.IO.File.Copy(this.path, backupFilePath, true);
                return new(backupFilePath);
            }
            catch {
                throw new Exception("Podana ścieżka jest nieprawidłowa. Podaj poprawną ścieżkę");
            }
        }
        //0 - get filename without its extension 1 - get directory name of file 2 - get file's extension
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
        public void look() { 
            //
        }
    }
}
