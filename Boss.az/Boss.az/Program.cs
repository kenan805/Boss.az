using Boss.az.Menu_side;
using System;
using System.Text;

namespace Boss.az
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.InputEncoding = Encoding.Unicode;
                Console.OutputEncoding = Encoding.Unicode;
                ProgramManagment.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}


