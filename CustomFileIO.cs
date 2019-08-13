using System;
using System.IO;
using System.Text;

namespace ShowIPtoConsole
{
    /// <summary>
    /// Collection of methods with files;
    /// Created by Sviatoslav Sudilovskiy.
    /// </summary>
    /// <description>
    /// If You add this class to your project, 
    /// You can work with files "out of box"
    /// </description>
    static class CustomFileIO
    {
        public static Encoding Current_enc {get{ return Encoding.Unicode;}}

        const string Title = "external IP";
        public enum SupportedExtensions { txt, html, fb2 };

        public static string CorrectFileName(string file_name, SupportedExtensions wanted_extension){ return file_name+"."+wanted_extension; }

        /// <summary>
        /// try read file, if failed, return message of error
        /// </summary>
        /// <param name="file_name">text.txt</param>
		/// <param name = "wanted_extension">selected extension</param>
        /// <returns>line that contains useful info</returns>
        public static string ReadContentFile(string file_name, SupportedExtensions wanted_extension)
        {
            try
            {
                byte index_of_content = 0;
                switch (wanted_extension)
                {
                    case (SupportedExtensions.txt): index_of_content = 0; break;
                    case (SupportedExtensions.html): index_of_content = 2; break;
                    case (SupportedExtensions.fb2): index_of_content = 7; break;
                }
                return File.ReadAllLines(CorrectFileName(file_name, wanted_extension), Encoding.Unicode)[index_of_content];
            }
            catch (Exception e) { return "Reading failed!" + "\r\n" + e.GetType() + "\r\n" + e.Message; };
        }

        public static bool BrutforceWriteFile(string wanted_file_name, string content, SupportedExtensions wanted_extension)
        {
            switch (wanted_extension)
            {
                case (SupportedExtensions.txt): return BrutforceWriteFile(wanted_file_name, new[] { content }, wanted_extension);
                case (SupportedExtensions.html): return BrutforceWriteFile(wanted_file_name, new[] { "<head><title>" + Title + "</title> <meta http-equiv=\"Content-Type\" content=\"text/html;charset=Windows-1251\"></head>", "<body>", content, "</body>" }, wanted_extension);
                case (SupportedExtensions.fb2):
                    {
                        return BrutforceWriteFile(wanted_file_name,
                            new[] { "<FictionBook>",
                            " <description>",
                            "  <book-title>",
                            "   "+Title,
                            "  </book-title>",
                            " </description>",
                            " <body><p>",
                            content,
                            " </p></body>",
                            "</FictionBook>",
                            }, wanted_extension);
                    }
                default: throw new NotSupportedException("Unknown format");
            }
        }


        public static bool BrutforceWriteFile(string wanted_file_name, string[] content, SupportedExtensions wanted_extension)
        {
			try
			{
				File.WriteAllLines(CorrectFileName(wanted_file_name, wanted_extension), content, Current_enc);
				return true;
			} catch {return false;}
        }

        /* TODO: implement SoftWriteFile
        /// <summary>
        /// function that try write text to filename_17.txt
        /// </summary>
        /// <param name="wanted_file_name"></param>
        /// <param name="content">strings into file</param>
        /// <returns>is equal wanted name of file and real name of file</returns>
        public static bool SoftWriteFile(string wanted_file_name, string[] content, SupportedExtensions wanted_extension)
        {
            string file_path = CorrectFileName(wanted_file_name, wanted_extension);
            bool file_yet_exist = File.Exists(file_path);
            File.WriteAllLines((file_yet_exist && !CustomIO.AskBoolQuestion("Rewrite yet existed file")) ? CustomIO.PleaseInput("new name of target file"):file_path, content, current_enc);
            return !file_yet_exist;
        }*/
    }
}
