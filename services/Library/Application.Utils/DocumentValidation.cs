using Application.Extensions;

namespace Application.Utils;

public class DocumentValidation
{
    private static int ValidateCpfDigit(int digit) => digit >= 10 ? 0 : digit;
    private static int GetRest(long digit) => Convert.ToInt32((digit * 10) % 11);

    public static bool ValidateCpf(string cpf)
    {
        int[] digits = (string.IsNullOrEmpty(cpf) ? "" : cpf).ToIntArray();

        if (digits.Length != 11) 
            return false;

        int[] validations = new int[2] { digits.Slice(9, 1).First(), digits.Slice(10, 1).First() };
        List<long> calculated = new List<long>() { 0, 0 };

        var v1 = digits.Skip(0).Take(9).ToList();
        for (int x = 0; x < v1.Count; x++)
            calculated[0] += v1[x] * (10 - x);

        if (validations.First() != ValidateCpfDigit(GetRest(calculated.First())))
            return false;

        var v2 = digits.Skip(0).Take(10).ToList();
        for (int x = 0; x < v2.Count; x++)
            calculated[1] += v2[x] * (11 - x);

        return validations.First() == ValidateCpfDigit(GetRest(calculated.First())) && validations.Last() == ValidateCpfDigit(GetRest(calculated.Last()));
    }

    private static int ValidateCnpjDigit(int calc)
    {
        return calc % 11 < 2 ? 0 : 11 - calc % 11;
    }

    /* [TODO]: Falta criar a lógica de validação */
    public static bool validateCNPJ(string cnpj)
    {
        int[] cnpjArray = (string.IsNullOrEmpty(cnpj) ? "" : cnpj).ToIntArray();

        if (cnpjArray.Length != 14)
            return false;

        var tamanhoTotal = cnpjArray.Length - 2;
        var digitosVerificadores = cnpjArray.Slice(tamanhoTotal, 2);

        var soma = 0;
        var pos = tamanhoTotal - 7;
        for (int i = tamanhoTotal; i >= 1; i--)
        {
            soma += cnpjArray[tamanhoTotal - i] * pos--;
            if (pos < 2)
                pos = 9;
        }

        if (ValidateCnpjDigit(soma) != digitosVerificadores[0])
            return false;

        tamanhoTotal = tamanhoTotal + 1;

        soma = 0;
        pos = tamanhoTotal - 7;
        for (int i = tamanhoTotal; i >= 1; i--)
        {
            soma += cnpjArray[tamanhoTotal - i] * pos--;
            if (pos < 2)
                pos = 9;
        }

        if (ValidateCnpjDigit(soma) != digitosVerificadores[1])
            return false;

        return true;
    }
}
