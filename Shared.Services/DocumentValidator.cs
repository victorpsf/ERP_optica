using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Services
{
    public static class DocumentValidator
    {
        private static int ValidateCpfCalc(int baseCalc, String partCpf)
        {
            int resultCalc = 0;

            for (int x = 0; x < partCpf.Length; x++)
            {
                int number = int.Parse(partCpf[x].ToString());
                resultCalc += number * (baseCalc - x);
            }

            return resultCalc;
        }

        private static int ValidateCpfCalcRest(int value) => ((value * 10) % 11);

        public static void ValidateCpf(String cpf, out String cpfFiltred, out bool Valid)
        {
            cpfFiltred = string.Join(
                "", 
                cpf.ToCharArray()
                    .Where(a => Regex.Match(a.ToString(), @"^(\d)$").Success)
                    .Select(a => a.ToString()
                )
            );

            if (cpfFiltred.Length != 11)
            {
                Valid = false;
                return;
            }

            int calc1 = ValidateCpfCalc(10, cpfFiltred.Substring(0, 9));
            int v1 = Convert.ToInt32(cpfFiltred.Substring(9, 1));
            int c1 = ValidateCpfCalcRest(calc1);

            int calc2 = ValidateCpfCalc(11, cpfFiltred.Substring(0, 10));
            int v2 = Convert.ToInt32(cpfFiltred.Substring(10, 1));
            int c2 = ValidateCpfCalcRest(calc2);

            Valid = (c1 == v1 && c2 == v2);
        }
    }
}
