using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptografyLab2
{
    class Alphabet
    {
        int[] colFx;
        double[] verSim;
        char[] cyrillic = {'А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т','У','Ф','Х','Ц','Ч',
                           'Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я'};
        char[] latin = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'V', 'X', 'Y', 'Z' };
        char[] binary = {'0','1'};

        public char[] Cyrillic { get => cyrillic; set => cyrillic = value; }
        public char[] Latin { get => latin; set => latin = value; }
        public char[] Binary { get => binary; set => binary = value; }

        public double Entropy(String file,char [] alf)
        {
            colFx = new int[alf.Length];
            for (int i = 0; i < alf.Length;i++)
            {
                colFx[i]=file.ToUpper().Where(el => el == alf[i]).Count();
                if(colFx[i]!=0)
                Console.WriteLine(colFx[i]+" "+alf[i]);
            }
            verSim = new double[alf.Length];
            for (int i = 0; i < alf.Length; i++)
            {
                if (colFx[i] != 0)
                {
                    verSim[i] = ((double)colFx[i] / file.Length);
                    Console.WriteLine(verSim[i]+" "+alf[i]);
                }
            }
            double res=0;
            for(int i=0;i<alf.Length;i++)
            {
                if(verSim[i]!=0)
                res += verSim[i] * Math.Log(verSim[i],2);
            }
            return (-res);
        }
        public double kolInf(double ResEntr,int kolSimv)
        {
            return ResEntr * (double)kolSimv;
        }
        public int [] GetASCII(String str)
        {
            int[] codASCII=new int [str.Length];
            for(int i=0;i<str.Length;i++)
            {
                codASCII[i] = ((int)str[i]);
            }
            return codASCII;
        }
        public string getBytes(int[] cod)
        {
            String strB = "";
            for(int i=0;i<cod.Length;i++)
            {
                strB += Convert.ToString(cod[i], 2)+" ";
            }
            return strB;
        }
        public double EfectEntropy(double verError)
        {
            return 1 - (-verError * Math.Log(verError, 2) - (1 - verError) * Math.Log((1 - verError), 2));
        }
        public void BuildExel(char[] alf)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Worksheet1");
                var headerRow = new List<object[]>();
                headerRow.Add(new object[] { "NAME", "Value" });
                for (int i=0;i<colFx.Length;i++)
                {
                    if(colFx[i]!=0)
                    headerRow.Add(new object[] { $"{alf[i]}", colFx[i] });
                }
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                var worksheet = excel.Workbook.Worksheets["Worksheet1"];
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                FileInfo excelFile = new FileInfo(@"D:\colSimv.xlsx");
                excel.SaveAs(excelFile);
            }
        }
    }
}
