using System;
using System.IO;

namespace VCN64Config
{
    public static class Validator
    {
        public static bool Evaluate(string filename)
        {
            bool result = false;

            if (System.IO.File.Exists(filename))
            {
                StreamReader source = null;
                try
                {
                    source = System.IO.File.OpenText(filename);
                    Syn analizer = new Syn(source);
                    VCN64Config.File config = analizer.Run();
                    result = true;
                    Console.WriteLine("The file \"" + filename + "\" is valid!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file \"" + filename + "\" is invalid.\n" + e.Message);
                }
                finally
                {
                    if (source != null)
                        source.Close();
                }
            }
            else
                Console.WriteLine("The file \"" + filename + "\" not exists.");

            return result;
        }
    }
}
