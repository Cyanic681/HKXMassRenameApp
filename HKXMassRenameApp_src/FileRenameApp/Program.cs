// See https://aka.ms/new-console-template for more information

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the HKX File Rename Application.");
            Console.WriteLine("Default directory name: filespath");
            Console.WriteLine("yabberbnd.xml will be created in the given directory.");
            Console.WriteLine("Please type in the directory path as text:");
            string directory = Console.ReadLine();
            List<string> listFileNames = CreateFileNameList(directory);
            GetListElements(listFileNames);
            RenameFiles(listFileNames, directory);
        }

        private static void RenameFiles(List<string> listFileNames, string directory)
        {
            Console.WriteLine("Enter an index of a char that should be changed within all file names in this folder:");
            string indexCharacter = Console.ReadLine();
            if (indexCharacter == null || (int.TryParse(indexCharacter, out int result) == false))
            {
                Console.WriteLine("No integer value found.");
                RenameFiles(listFileNames, directory);
            }
            else
            {
                Console.WriteLine("Enter a new character:");
            }

            var newCharacter = Console.ReadLine();
            int index = int.Parse(indexCharacter);
            string content = null;
            foreach (string filename in listFileNames)
            {
                var newFilename = filename.Substring(0,index) + newCharacter + filename.Substring(index+1);
                Console.WriteLine("New file name =" + newFilename);
                Directory.Move(directory+"\\"+filename, directory + "\\" + newFilename);

                string fileId = "1"+newFilename.Substring(1,3)+newFilename.Substring(5, 6);
                content += $"<file>\r\n  <flags>0x40</flags>\r\n  <id>{fileId}</id>\r\n  <root>N:\\FDP\\data\\INTERROOT_win64\\</root>\r\n  <path>chr\\c1510\\hkx\\{newFilename}</path>\r\n</file>\r\n";
            }

            var xmldirectory = directory + "\\yabberbnd";
            if (!(Directory.Exists(xmldirectory)) && Directory.Exists(directory))
            {
                Directory.CreateDirectory(xmldirectory);
            }
            if (File.Exists(xmldirectory + "\\yabberbnd.xml"))
            {
                File.Delete(xmldirectory + "\\yabberbnd.xml");
            }
            if (Directory.Exists(xmldirectory))
            {
                File.WriteAllText(xmldirectory + "\\yabberbnd.xml", content);
            }
        }

        private static List<string> CreateFileNameList(string directory)
        {
            string filename;
            var listFileNames = new List<string>();

            if (Directory.Exists(directory))
            {
                string[] filePaths = Directory.GetFiles(directory);
                foreach (string filePath in filePaths)
                {
                    filename = System.IO.Path.GetFileName(filePath);
                    listFileNames.Add(filename);
                }
            }
            return listFileNames;
        }

        private static void GetListElements(List<string> listFilePaths)
        {
            var fileAmount = listFilePaths.Count();
            int i = 0;
            while (i < fileAmount)
            {
                Console.WriteLine(listFilePaths.ElementAt(i));
                i += 1;
            }
        }
    }
}
